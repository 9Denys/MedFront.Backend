using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Request.Queries
{
    public class GetAllRequestsQuery : IRequest<List<RequestDto>>
    {
    }

    public class GetAllRequestsQueryHandler : IRequestHandler<GetAllRequestsQuery, List<RequestDto>>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;

        public GetAllRequestsQueryHandler(IMedFrontDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<RequestDto>> Handle(GetAllRequestsQuery request, CancellationToken ct)
        {
            var items = await _context.Requests
                .AsNoTracking()
                .Include(r => r.Warehouse)
                .Include(r => r.Medication)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync(ct);

            return _mapper.Map<List<RequestDto>>(items);
        }
    }
}
