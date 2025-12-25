using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.UpdateDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Medication.Commands
{
    public class UpdateMedicationCommand : IRequest
    {
        public Guid Id { get; }
        public MedicationUpdateDto Dto { get; }

        public UpdateMedicationCommand(Guid id, MedicationUpdateDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }

    public class UpdateMedicationCommandHandler : IRequestHandler<UpdateMedicationCommand>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;

        public UpdateMedicationCommandHandler(IMedFrontDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Handle(UpdateMedicationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Medications
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

            if (entity is null)
                throw new KeyNotFoundException("Medication not found");

            var skuUsedByOther = await _context.Medications
                .AnyAsync(m => m.SKU == request.Dto.SKU && m.Id != request.Id, cancellationToken);

            if (skuUsedByOther)
                throw new InvalidOperationException("Medication with this SKU already exists");

            _mapper.Map(request.Dto, entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
