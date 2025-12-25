using MedFront.Backend.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Warehouse
{
    public class WarehouseOccupancyService
    {
        private readonly IMedFrontDbContext _context;

        public WarehouseOccupancyService(IMedFrontDbContext context)
        {
            _context = context;
        }


        public async Task<decimal> CalculateAsync(Guid warehouseId, CancellationToken ct)
        {
            var occupancy = await _context.MedicationStocks
                .AsNoTracking()
                .Where(ms => ms.WarehouseId == warehouseId)
                .Include(ms => ms.Medication)
                .Select(ms => (decimal)ms.BoxQuantity * ms.Medication.PackageVolume)
                .DefaultIfEmpty(0m)
                .SumAsync(ct);

            return occupancy;
        }



        public async Task RecalculateAndPersistAsync(Guid warehouseId, CancellationToken ct)
        {
            var occupancy = await CalculateAsync(warehouseId, ct);

            var warehouse = await _context.Warehouses
                .FirstAsync(w => w.Id == warehouseId, ct);

            warehouse.CurrentOccupancy = occupancy;

            await _context.SaveChangesAsync(ct);
        }

        public async Task EnsureCapacityAsync(Guid warehouseId, decimal deltaVolume, CancellationToken ct)
        {
            var warehouse = await _context.Warehouses
                .AsNoTracking()
                .FirstAsync(w => w.Id == warehouseId, ct);

            var current = await CalculateAsync(warehouseId, ct);

            if (current + deltaVolume > warehouse.TotalVolume)
                throw new InvalidOperationException("Warehouse capacity exceeded.");
        }

        public async Task<decimal> CalculateDeltaAsync(Guid medicationId, int boxQuantity, CancellationToken ct)
        {
            var packageVolume = await _context.Medications
                .AsNoTracking()
                .Where(m => m.Id == medicationId)
                .Select(m => m.PackageVolume)
                .FirstAsync(ct);

            return packageVolume * boxQuantity;
        }
    }
}
