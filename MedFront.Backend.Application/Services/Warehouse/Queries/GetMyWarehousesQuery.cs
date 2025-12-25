using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Warehouse.Queries
{
    public class GetMyWarehousesQuery : IRequest<List<WarehouseDto>>
    {
    }

    public class GetMyWarehousesQueryHandler : IRequestHandler<GetMyWarehousesQuery, List<WarehouseDto>>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IUserContextService _userContext;
        private readonly IMapper _mapper;

        public GetMyWarehousesQueryHandler(IMedFrontDbContext context, IUserContextService userContext, IMapper mapper)
        {
            _context = context;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<List<WarehouseDto>> Handle(GetMyWarehousesQuery request, CancellationToken cancellationToken)
        {
            if (!_userContext.IsAuthenticated())
                throw new UnauthorizedAccessException("User is not authenticated");

            var user = _userContext.GetCurrentUser();
            var userId = _userContext.GetCurrentUserId();

            if (userId is null)
                throw new UnauthorizedAccessException("User is not authenticated");

            if (user?.IsInRole("Admin") == true)
            {
                var all = await _context.Warehouses
                    .AsNoTracking()
                    .OrderBy(w => w.Address)
                    .ToListAsync(cancellationToken);

                return _mapper.Map<List<WarehouseDto>>(all);
            }

            var warehouseIds = await _context.WarehouseAccesses
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => x.WarehouseId)
                .Distinct()
                .ToListAsync(cancellationToken);

            var myWarehouses = await _context.Warehouses
                .AsNoTracking()
                .Where(w => warehouseIds.Contains(w.Id))
                .OrderBy(w => w.Address)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<WarehouseDto>>(myWarehouses);
        }
    }
}
