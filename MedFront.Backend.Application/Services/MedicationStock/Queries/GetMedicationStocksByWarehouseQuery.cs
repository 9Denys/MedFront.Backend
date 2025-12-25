using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.MedicationStock.Queries
{
    public class GetMedicationStocksByWarehouseQuery : IRequest<List<MedicationStockDto>>
    {
        public Guid WarehouseId { get; }

        public GetMedicationStocksByWarehouseQuery(Guid warehouseId)
        {
            WarehouseId = warehouseId;
        }
    }

    public class GetMedicationStocksByWarehouseQueryHandler
        : IRequestHandler<GetMedicationStocksByWarehouseQuery, List<MedicationStockDto>>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IWarehouseAccessService _access;
        private readonly IMapper _mapper;

        public GetMedicationStocksByWarehouseQueryHandler(
            IMedFrontDbContext context,
            IWarehouseAccessService access,
            IMapper mapper)
        {
            _context = context;
            _access = access;
            _mapper = mapper;
        }

        public async Task<List<MedicationStockDto>> Handle(GetMedicationStocksByWarehouseQuery request, CancellationToken cancellationToken)
        {
            await _access.EnsureAccessAsync(request.WarehouseId, cancellationToken);

            var stocks = await _context.MedicationStocks
                .AsNoTracking()
                .Include(x => x.Medication)
                .Where(x => x.WarehouseId == request.WarehouseId)
                .OrderBy(x => x.Medication.Name)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<MedicationStockDto>>(stocks);
        }
    }
}
