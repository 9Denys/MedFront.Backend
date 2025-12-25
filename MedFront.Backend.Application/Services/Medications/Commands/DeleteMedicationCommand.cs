using MedFront.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Medication.Commands
{
    public class DeleteMedicationCommand : IRequest
    {
        public Guid Id { get; }

        public DeleteMedicationCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteMedicationCommandHandler : IRequestHandler<DeleteMedicationCommand>
    {
        private readonly IMedFrontDbContext _context;

        public DeleteMedicationCommandHandler(IMedFrontDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteMedicationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Medications
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

            if (entity is null)
                throw new KeyNotFoundException("Medication not found");

            _context.Medications.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
