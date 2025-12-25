using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Sensor.Queries
{
    public class GetSensorsByWarehouseQuery : IRequest<List<SensorDto>>
    {
        public Guid WarehouseId { get; }
        public GetSensorsByWarehouseQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }

    public class GetSensorsByWarehouseQueryHandler : IRequestHandler<GetSensorsByWarehouseQuery, List<SensorDto>>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;

        public GetSensorsByWarehouseQueryHandler(IMedFrontDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SensorDto>> Handle(GetSensorsByWarehouseQuery request, CancellationToken ct)
        {
            var items = await _context.Sensors
                .AsNoTracking()
                .Include(s => s.Warehouse)
                .Where(s => s.WarehouseId == request.WarehouseId)
                .OrderBy(s => s.SerialName)
                .ToListAsync(ct);

            return _mapper.Map<List<SensorDto>>(items);
        }
    }
}
