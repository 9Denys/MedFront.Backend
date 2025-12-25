using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Warehouse.Queries
{
    public class GetAllWarehousesQuery : IRequest<List<WarehouseDto>>
    {
    }

    public class GetAllWarehousesQueryHandler : IRequestHandler<GetAllWarehousesQuery, List<WarehouseDto>>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;

        public GetAllWarehousesQueryHandler(IMedFrontDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<WarehouseDto>> Handle(GetAllWarehousesQuery request, CancellationToken cancellationToken)
        {
            var warehouses = await _context.Warehouses
                .AsNoTracking()
                .OrderBy(w => w.Address)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<WarehouseDto>>(warehouses);
        }
    }
}
