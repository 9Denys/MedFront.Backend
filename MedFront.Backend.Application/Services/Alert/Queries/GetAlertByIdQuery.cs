using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Alert.Queries
{
    public class GetAlertByIdQuery : IRequest<AlertDto>
    {
        public Guid Id { get; }
        public GetAlertByIdQuery(Guid id) => Id = id;
    }

    public class GetAlertByIdQueryHandler : IRequestHandler<GetAlertByIdQuery, AlertDto>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;

        public GetAlertByIdQueryHandler(IMedFrontDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AlertDto> Handle(GetAlertByIdQuery request, CancellationToken ct)
        {
            var alert = await _context.Alerts
                .Include(a => a.Medication)
                .FirstOrDefaultAsync(a => a.Id == request.Id, ct);

            if (alert == null)
                throw new Exception($"Alert with id {request.Id} was not found.");

            if (!alert.IsRead)
            {
                alert.IsRead = true;
                alert.ReadAt = DateTime.UtcNow;

                await _context.SaveChangesAsync(ct);
            }

            return _mapper.Map<AlertDto>(alert);
        }
    }
}
