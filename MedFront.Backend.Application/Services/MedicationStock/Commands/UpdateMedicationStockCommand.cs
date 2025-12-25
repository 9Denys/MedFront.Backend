using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Application.Services.Warehouse;
using MedFront.Backend.Contracts.DTOs.UpdateDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.MedicationStock.Commands
{
    public class UpdateMedicationStockCommand : IRequest<Unit>
    {
        public Guid StockId { get; }
        public UpdateMedicationStockDto Dto { get; }

        public UpdateMedicationStockCommand(Guid stockId, UpdateMedicationStockDto dto)
        {
            StockId = stockId;
            Dto = dto;
        }
    }

    public class UpdateMedicationStockCommandHandler : IRequestHandler<UpdateMedicationStockCommand, Unit>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;
        private readonly WarehouseOccupancyService _occupancyService;
        private readonly IAlertService _alertService;

        public UpdateMedicationStockCommandHandler(
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

        public async Task<Unit> Handle(UpdateMedicationStockCommand request, CancellationToken cancellationToken)
        {
            var stock = await _context.MedicationStocks
                .FirstOrDefaultAsync(x => x.Id == request.StockId, cancellationToken);

            if (stock is null)
                throw new KeyNotFoundException("MedicationStock not found.");

            var oldBoxQuantity = stock.BoxQuantity;

            _mapper.Map(request.Dto, stock);

            if (stock.BoxQuantity > oldBoxQuantity)
            {
                var increase = stock.BoxQuantity - oldBoxQuantity;

                var delta = await _occupancyService.CalculateDeltaAsync(
                    stock.MedicationId,
                    increase,
                    cancellationToken);

                await _occupancyService.EnsureCapacityAsync(
                    stock.WarehouseId,
                    delta,
                    cancellationToken);
            }

            await _alertService.CheckAndCreateAlertsAsync(stock, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            if (stock.BoxQuantity != oldBoxQuantity)
            {
                await _occupancyService.RecalculateAndPersistAsync(stock.WarehouseId, cancellationToken);
            }

            return Unit.Value;
        }
    }
}
