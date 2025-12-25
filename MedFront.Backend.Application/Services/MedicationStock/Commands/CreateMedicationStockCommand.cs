using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Application.Services.Warehouse;
using MedFront.Backend.Contracts.DTOs.CreateDTOs;
using MedFront.Backend.Contracts.DTOs.MedicationStock;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.MedicationStock.Commands
{
    public class CreateMedicationStockCommand : IRequest<Guid>
    {
        public CreateMedicationStockDto Dto { get; }

        public CreateMedicationStockCommand(CreateMedicationStockDto dto)
        {
            Dto = dto;
        }
    }

    public class CreateMedicationStockCommandHandler : IRequestHandler<CreateMedicationStockCommand, Guid>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;
        private readonly WarehouseOccupancyService _occupancyService;
        private readonly IAlertService _alertService;

        public CreateMedicationStockCommandHandler(
            IMedFrontDbContext context,
            IMapper mapper,
            WarehouseOccupancyService occupancyService,
            IAlertService alertService)
        {
            _context = context;
            _mapper = mapper;
            _occupancyService = occupancyService;
            _alertService = alertService;
        }

        public async Task<Guid> Handle(CreateMedicationStockCommand request, CancellationToken cancellationToken)
        {
            var warehouseExists = await _context.Warehouses
                .AsNoTracking()
                .AnyAsync(x => x.Id == request.Dto.WarehouseId, cancellationToken);

            if (!warehouseExists)
                throw new KeyNotFoundException("Warehouse not found.");

            var medicationExists = await _context.Medications
                .AsNoTracking()
                .AnyAsync(x => x.Id == request.Dto.MedicationId, cancellationToken);

            if (!medicationExists)
                throw new KeyNotFoundException("Medication not found.");

            var delta = await _occupancyService.CalculateDeltaAsync(
                request.Dto.MedicationId,
                request.Dto.BoxQuantity,
                cancellationToken);

            await _occupancyService.EnsureCapacityAsync(
                request.Dto.WarehouseId,
                delta,
                cancellationToken);

            var entity = _mapper.Map<Domain.Entities.MedicationStock>(request.Dto);

            _context.MedicationStocks.Add(entity);

            await _alertService.CheckAndCreateAlertsAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            await _occupancyService.RecalculateAndPersistAsync(entity.WarehouseId, cancellationToken);

            return entity.Id;
        }
    }
}
