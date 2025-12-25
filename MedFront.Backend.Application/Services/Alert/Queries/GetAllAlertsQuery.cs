using AutoMapper;
using AutoMapper.QueryableExtensions;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Alert.Queries
{
    public class GetAllAlertsQuery : IRequest<List<AlertDto>> { }

    public class GetAllAlertsQueryHandler : IRequestHandler<GetAllAlertsQuery, List<AlertDto>>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;

        public GetAllAlertsQueryHandler(IMedFrontDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AlertDto>> Handle(GetAllAlertsQuery request, CancellationToken ct)
        {
            var query = _context.Alerts
                .Include(a => a.Medication);

            return await query
                .OrderBy(a => a.IsRead)
                .ThenByDescending(a => a.CreatedAt)
                .AsNoTracking()
                .ProjectTo<AlertDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);
        }
    }
}
