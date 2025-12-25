using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.UpdateDTOs;
using MedFront.Backend.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Sensor.Commands
{
    public class UpdateSensorThresholdsCommand : IRequest
    {
        public Guid SensorId { get; }
        public SensorThresholdUpdateDto Dto { get; }

        public UpdateSensorThresholdsCommand(Guid sensorId, SensorThresholdUpdateDto dto)
        {
            SensorId = sensorId;
            Dto = dto;
        }
    }

    public class UpdateSensorThresholdsCommandHandler : IRequestHandler<UpdateSensorThresholdsCommand>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;

        public UpdateSensorThresholdsCommandHandler(IMedFrontDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Handle(UpdateSensorThresholdsCommand request, CancellationToken ct)
        {
            var sensor = await _context.Sensors
                .FirstOrDefaultAsync(s => s.Id == request.SensorId, ct);

            if (sensor is null)
                throw new Exception("Sensor not found.");

            if (sensor.SensorType != SensorType.Temperature)
                throw new Exception("Thresholds can be updated only for Temperature sensors.");

            _mapper.Map(request.Dto, sensor);

            await _context.SaveChangesAsync(ct);
        }
    }
}
