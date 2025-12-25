using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Request.Queries
{
    public class GetRequestByIdQuery : IRequest<RequestDto>
    {
        public Guid Id { get; }

        public GetRequestByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetRequestByIdQueryHandler : IRequestHandler<GetRequestByIdQuery, RequestDto>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;

        public GetRequestByIdQueryHandler(IMedFrontDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RequestDto> Handle(GetRequestByIdQuery request, CancellationToken ct)
        {
            var entity = await _context.Requests
                .AsNoTracking()
                .Include(r => r.Warehouse)
                .Include(r => r.Medication)
                .FirstOrDefaultAsync(r => r.Id == request.Id, ct);

            if (entity is null)
                throw new Exception("Request not found.");

            return _mapper.Map<RequestDto>(entity);
        }
    }
}
