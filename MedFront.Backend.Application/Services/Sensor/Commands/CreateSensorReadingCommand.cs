using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.CreateDTOs;
using MedFront.Backend.Domain.Entities;
using MedFront.Backend.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Sensor.Commands
{
    public class CreateSensorReadingCommand : IRequest<Guid>
    {
        public SensorReadingCreateDto Dto { get; }
        public CreateSensorReadingCommand(SensorReadingCreateDto dto) => Dto = dto;
    }

    public class CreateSensorReadingCommandHandler : IRequestHandler<CreateSensorReadingCommand, Guid>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;

        public CreateSensorReadingCommandHandler(IMedFrontDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateSensorReadingCommand request, CancellationToken ct)
        {
            var sensor = await _context.Sensors
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == request.Dto.SensorId, ct);

            if (sensor is null)
                throw new Exception("Sensor not found.");

            if (sensor.SensorType != SensorType.Temperature)
                throw new Exception("Readings can be created only for Temperature sensors.");

            var entity = _mapper.Map<SensorReading>(request.Dto);

            entity.Time = request.Dto.Time?.ToUniversalTime() ?? DateTime.UtcNow;

            _context.SensorReadings.Add(entity);
            await _context.SaveChangesAsync(ct);

            return entity.Id;
        }
    }
}
