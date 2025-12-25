using System;
using System.Threading;
using System.Threading.Tasks;

namespace MedFront.Backend.Application.Interfaces
{
    public interface IWarehouseAccessService
    {
        Task<bool> HasAccessAsync(Guid warehouseId, CancellationToken ct);
        Task EnsureAccessAsync(Guid warehouseId, CancellationToken ct);

    }
}
