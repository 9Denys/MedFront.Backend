using MedFront.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.WarehouseAccess.Commands
{
    public class RevokeWarehouseAccessCommand : IRequest
    {
        public Guid Id { get; }

        public RevokeWarehouseAccessCommand(Guid id)
        {
            Id = id;
        }
    }

    public class RevokeWarehouseAccessCommandHandler : IRequestHandler<RevokeWarehouseAccessCommand>
    {
        private readonly IMedFrontDbContext _context;

        public RevokeWarehouseAccessCommandHandler(IMedFrontDbContext context)
        {
            _context = context;
        }

        public async Task Handle(RevokeWarehouseAccessCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.WarehouseAccesses
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)
                throw new KeyNotFoundException("Warehouse access not found");

            _context.WarehouseAccesses.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
