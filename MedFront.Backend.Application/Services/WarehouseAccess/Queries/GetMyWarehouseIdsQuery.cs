using MedFront.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.WarehouseAccess.Queries
{
    public class GetMyWarehouseIdsQuery : IRequest<List<Guid>>
    {
    }

    public class GetMyWarehouseIdsQueryHandler : IRequestHandler<GetMyWarehouseIdsQuery, List<Guid>>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IUserContextService _userContext;

        public GetMyWarehouseIdsQueryHandler(IMedFrontDbContext context, IUserContextService userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<List<Guid>> Handle(GetMyWarehouseIdsQuery request, CancellationToken cancellationToken)
        {
            if (!_userContext.IsAuthenticated())
                throw new UnauthorizedAccessException("User is not authenticated");

            var user = _userContext.GetCurrentUser();
            var userId = _userContext.GetCurrentUserId();

            if (userId is null)
                throw new UnauthorizedAccessException("User is not authenticated");

            if (user?.IsInRole("Admin") == true)
            {
                return await _context.Warehouses
                    .AsNoTracking()
                    .Select(w => w.Id)
                    .ToListAsync(cancellationToken);
            }

            return await _context.WarehouseAccesses
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => x.WarehouseId)
                .Distinct()
                .ToListAsync(cancellationToken);
        }
    }
}
