using MedFront.Backend.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MedFront.Backend.Application.Services.WarehouseAccess
{
    public class WarehouseAccessService : IWarehouseAccessService
    {
        private readonly IMedFrontDbContext _context;
        private readonly IUserContextService _userContext;

        public WarehouseAccessService(IMedFrontDbContext context, IUserContextService userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<bool> HasAccessAsync(Guid warehouseId, CancellationToken ct)
        {
            if (!_userContext.IsAuthenticated())
                return false;

            if (IsAdmin())
                return true;

            var userId = _userContext.GetCurrentUserId();
            if (userId is null)
                return false;

            return await _context.WarehouseAccesses
                .AsNoTracking()
                .AnyAsync(x => x.UserId == userId && x.WarehouseId == warehouseId, ct);
        }

        public async Task EnsureAccessAsync(Guid warehouseId, CancellationToken ct)
        {
            var ok = await HasAccessAsync(warehouseId, ct);
            if (!ok)
                throw new UnauthorizedAccessException("You don't have access to this warehouse.");
        }

        private bool IsAdmin()
        {
            var user = _userContext.GetCurrentUser();
            if (user is null) return false;

            if (user.IsInRole("Admin"))
                return true;

            var roleClaim = user.FindFirst(ClaimTypes.Role)?.Value;
            return string.Equals(roleClaim, "Admin", StringComparison.OrdinalIgnoreCase);
        }
    }
}
