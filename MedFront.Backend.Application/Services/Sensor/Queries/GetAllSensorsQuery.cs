using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Sensor.Queries
{
    public class GetAllSensorsQuery : IRequest<List<SensorDto>> { }

    public class GetAllSensorsQueryHandler : IRequestHandler<GetAllSensorsQuery, List<SensorDto>>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;

        public GetAllSensorsQueryHandler(IMedFrontDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SensorDto>> Handle(GetAllSensorsQuery request, CancellationToken ct)
        {
            var items = await _context.Sensors
                .AsNoTracking()
                .Include(s => s.Warehouse)
                .OrderBy(s => s.SerialName)
                .ToListAsync(ct);

            return _mapper.Map<List<SensorDto>>(items);
        }
    }
}
