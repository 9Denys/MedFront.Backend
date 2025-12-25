using MedFront.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.MedicationStock.Commands
{
    public class DeleteMedicationStockCommand : IRequest<Unit>
    {
        public Guid StockId { get; }

        public DeleteMedicationStockCommand(Guid stockId)
        {
            StockId = stockId;
        }
    }

    public class DeleteMedicationStockCommandHandler : IRequestHandler<DeleteMedicationStockCommand, Unit>
    {
        private readonly IMedFrontDbContext _context;

        public DeleteMedicationStockCommandHandler(IMedFrontDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteMedicationStockCommand request, CancellationToken cancellationToken)
        {
            var stock = await _context.MedicationStocks
                .FirstOrDefaultAsync(x => x.Id == request.StockId, cancellationToken);

            if (stock is null)
                throw new KeyNotFoundException("MedicationStock not found.");

            _context.MedicationStocks.Remove(stock);

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
