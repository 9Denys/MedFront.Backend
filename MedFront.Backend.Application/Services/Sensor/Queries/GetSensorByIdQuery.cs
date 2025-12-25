using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Sensor.Queries
{
    public class GetSensorByIdQuery : IRequest<SensorDto>
    {
        public Guid Id { get; }
        public GetSensorByIdQuery(Guid id) => Id = id;
    }

    public class GetSensorByIdQueryHandler : IRequestHandler<GetSensorByIdQuery, SensorDto>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;

        public GetSensorByIdQueryHandler(IMedFrontDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SensorDto> Handle(GetSensorByIdQuery request, CancellationToken ct)
        {
            var entity = await _context.Sensors
                .AsNoTracking()
                .Include(s => s.Warehouse)
                .FirstOrDefaultAsync(s => s.Id == request.Id, ct);

            if (entity is null)
                throw new Exception("Sensor not found.");

            return _mapper.Map<SensorDto>(entity);
        }
    }
}
