using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Medication.Queries
{
    public class GetAllMedicationsQuery : IRequest<List<MedicationDto>>
    {
    }

    public class GetAllMedicationsQueryHandler : IRequestHandler<GetAllMedicationsQuery, List<MedicationDto>>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;

        public GetAllMedicationsQueryHandler(IMedFrontDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MedicationDto>> Handle(GetAllMedicationsQuery request, CancellationToken cancellationToken)
        {
            var medications = await _context.Medications
                .AsNoTracking()
                .OrderBy(m => m.Name)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<MedicationDto>>(medications);
        }
    }
}
