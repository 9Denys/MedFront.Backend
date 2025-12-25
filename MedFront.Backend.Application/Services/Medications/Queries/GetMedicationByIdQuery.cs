using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Medication.Queries
{
    public class GetMedicationByIdQuery : IRequest<MedicationDto>
    {
        public Guid Id { get; }

        public GetMedicationByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetMedicationByIdQueryHandler : IRequestHandler<GetMedicationByIdQuery, MedicationDto>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;

        public GetMedicationByIdQueryHandler(IMedFrontDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MedicationDto> Handle(GetMedicationByIdQuery request, CancellationToken cancellationToken)
        {
            var medication = await _context.Medications
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

            if (medication is null)
                throw new KeyNotFoundException("Medication not found");

            return _mapper.Map<MedicationDto>(medication);
        }
    }
}
