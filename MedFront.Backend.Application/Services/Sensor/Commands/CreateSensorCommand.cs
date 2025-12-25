using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.CreateDTOs;
using MedFront.Backend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Sensor.Commands
{
    public class CreateSensorCommand : IRequest<Guid>
    {
        public SensorCreateDto Dto { get; }
        public CreateSensorCommand(SensorCreateDto dto) => Dto = dto;
    }

    public class CreateSensorCommandHandler : IRequestHandler<CreateSensorCommand, Guid>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;

        public CreateSensorCommandHandler(IMedFrontDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateSensorCommand request, CancellationToken ct)
        {
            var warehouseExists = await _context.Warehouses
                .AsNoTracking()
                .AnyAsync(w => w.Id == request.Dto.WarehouseId, ct);

            if (!warehouseExists)
                throw new Exception("Warehouse not found.");

            var serialTaken = await _context.Sensors
                .AsNoTracking()
                .AnyAsync(s => s.SerialName == request.Dto.SerialName, ct);

            if (serialTaken)
                throw new Exception("Sensor with this SerialName already exists.");

            var entity = _mapper.Map<Domain.Entities.Sensor>(request.Dto);

            _context.Sensors.Add(entity);
            await _context.SaveChangesAsync(ct);

            return entity.Id;
        }
    }
}
