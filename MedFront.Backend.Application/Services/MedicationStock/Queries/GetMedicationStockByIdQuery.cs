using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.MedicationStock.Queries
{
    public class GetMedicationStockByIdQuery : IRequest<MedicationStockDto>
    {
        public Guid StockId { get; }

        public GetMedicationStockByIdQuery(Guid stockId)
        {
            StockId = stockId;
        }
    }

    public class GetMedicationStockByIdQueryHandler : IRequestHandler<GetMedicationStockByIdQuery, MedicationStockDto>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IWarehouseAccessService _access;
        private readonly IMapper _mapper;

        public GetMedicationStockByIdQueryHandler(IMedFrontDbContext context, IWarehouseAccessService access, IMapper mapper)
        {
            _context = context;
            _access = access;
            _mapper = mapper;
        }

        public async Task<MedicationStockDto> Handle(GetMedicationStockByIdQuery request, CancellationToken cancellationToken)
        {
            var stock = await _context.MedicationStocks
                .Include(x => x.Medication)
                .FirstOrDefaultAsync(x => x.Id == request.StockId, cancellationToken);

            if (stock is null)
                throw new KeyNotFoundException("MedicationStock not found.");

            await _access.EnsureAccessAsync(stock.WarehouseId, cancellationToken);

            return _mapper.Map<MedicationStockDto>(stock);
        }
    }
}
