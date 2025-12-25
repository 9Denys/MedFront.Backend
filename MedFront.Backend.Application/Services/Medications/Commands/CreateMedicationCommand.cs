using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.CreateDTOs;
using MedFront.Backend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Medication.Commands
{
    public class CreateMedicationCommand : IRequest<Guid>
    {
        public MedicationCreateDto Dto { get; }

        public CreateMedicationCommand(MedicationCreateDto dto)
        {
            Dto = dto;
        }
    }

    public class CreateMedicationCommandHandler : IRequestHandler<CreateMedicationCommand, Guid>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;

        public CreateMedicationCommandHandler(IMedFrontDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateMedicationCommand request, CancellationToken cancellationToken)
        {
            var skuExists = await _context.Medications
                .AnyAsync(m => m.SKU == request.Dto.SKU, cancellationToken);

            if (skuExists)
                throw new InvalidOperationException("Medication with this SKU already exists");

            var entity = _mapper.Map<Domain.Entities.Medication>(request.Dto);

            _context.Medications.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
