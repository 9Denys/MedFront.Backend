using MedFront.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Warehouse.Commands
{
    public class DeleteWarehouseCommand : IRequest
    {
        public Guid Id { get; }

        public DeleteWarehouseCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand>
    {
        private readonly IMedFrontDbContext _context;

        public DeleteWarehouseCommandHandler(IMedFrontDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Warehouses
                .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

            if (entity is null)
                throw new KeyNotFoundException("Warehouse not found");

            _context.Warehouses.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
