using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Application.Services.Warehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.MedicationStock.Commands
{
    public class WriteOffMedicationStockCommand : IRequest<Unit>
    {
        public Guid StockId { get; }
        public int Quantity { get; }

        public WriteOffMedicationStockCommand(Guid stockId, int quantity)
        {
            StockId = stockId;
            Quantity = quantity;
        }
    }

    public class WriteOffMedicationStockCommandHandler : IRequestHandler<WriteOffMedicationStockCommand, Unit>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IWarehouseAccessService _access;
        private readonly IAlertService _alertService;
        private readonly WarehouseOccupancyService _occupancyService;

        public WriteOffMedicationStockCommandHandler(
            IMedFrontDbContext context,
            IWarehouseAccessService access,
            IAlertService alertService,
            WarehouseOccupancyService occupancyService)
        {
            _context = context;
            _access = access;
            _alertService = alertService;
            _occupancyService = occupancyService;
        }

        public async Task<Unit> Handle(WriteOffMedicationStockCommand request, CancellationToken cancellationToken)
        {
            var stock = await _context.MedicationStocks
                .FirstOrDefaultAsync(x => x.Id == request.StockId, cancellationToken);

            if (stock is null)
                throw new KeyNotFoundException("MedicationStock not found.");

            await _access.EnsureAccessAsync(stock.WarehouseId, cancellationToken);

            if (request.Quantity <= 0)
                throw new ArgumentException("Quantity must be > 0.");

            if (stock.BoxQuantity < request.Quantity)
                throw new InvalidOperationException("Not enough stock to write off.");

            stock.BoxQuantity -= request.Quantity;

            await _alertService.CheckAndCreateAlertsAsync(stock, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            await _occupancyService.RecalculateAndPersistAsync(stock.WarehouseId, cancellationToken);

            return Unit.Value;
        }
    }
}
