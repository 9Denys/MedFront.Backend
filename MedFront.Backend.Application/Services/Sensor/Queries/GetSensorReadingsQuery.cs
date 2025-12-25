using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Sensor.Queries
{
    public class GetSensorReadingsQuery : IRequest<List<SensorReadingDto>>
    {
        public Guid SensorId { get; }
        public SensorReadingQueryDto Filter { get; }

        public GetSensorReadingsQuery(Guid sensorId, SensorReadingQueryDto filter)
        {
            SensorId = sensorId;
            Filter = filter;
        }
    }

    public class GetSensorReadingsQueryHandler : IRequestHandler<GetSensorReadingsQuery, List<SensorReadingDto>>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;

        public GetSensorReadingsQueryHandler(IMedFrontDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SensorReadingDto>> Handle(GetSensorReadingsQuery request, CancellationToken ct)
        {
            var q = _context.SensorReadings
                .AsNoTracking()
                .Where(r => r.SensorId == request.SensorId);

            if (request.Filter.FromUtc.HasValue)
                q = q.Where(r => r.Time >= request.Filter.FromUtc.Value);

            if (request.Filter.ToUtc.HasValue)
                q = q.Where(r => r.Time <= request.Filter.ToUtc.Value);

            var take = request.Filter.Take.HasValue && request.Filter.Take.Value > 0
                ? Math.Min(request.Filter.Take.Value, 5000)
                : 200;

            var items = await q
                .OrderByDescending(r => r.Time)
                .Take(take)
                .OrderBy(r => r.Time) 
                .ToListAsync(ct);

            return _mapper.Map<List<SensorReadingDto>>(items);
        }
    }
}
