using MedFront.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Interfaces
{
    public interface IMedFrontDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Warehouse> Warehouses { get; }
        DbSet<WarehouseAccess> WarehouseAccesses { get; }
        DbSet<Medication> Medications { get; }
        DbSet<MedicationStock> MedicationStocks { get; }
        DbSet<Sensor> Sensors { get; }
        DbSet<SensorReading> SensorReadings { get; }
        DbSet<Alert> Alerts { get; }
        DbSet<Request> Requests { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
