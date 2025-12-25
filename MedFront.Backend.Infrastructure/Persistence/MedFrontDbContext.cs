using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MedFront.Backend.Infrastructure.Persistence
{
        public class MedFrontDbContext : DbContext, IMedFrontDbContext

    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Warehouse> Warehouses { get; set; } = null!;
        public DbSet<WarehouseAccess> WarehouseAccesses { get; set; } = null!;
        public DbSet<Medication> Medications { get; set; } = null!;
        public DbSet<MedicationStock> MedicationStocks { get; set; } = null!;
        public DbSet<Sensor> Sensors { get; set; } = null!;
        public DbSet<SensorReading> SensorReadings { get; set; } = null!;
        public DbSet<Alert> Alerts { get; set; } = null!;
        public DbSet<Request> Requests { get; set; } = null!;

        public MedFrontDbContext(DbContextOptions<MedFrontDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MedFrontDbContext).Assembly);

            modelBuilder.Entity<User>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<Warehouse>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<WarehouseAccess>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<Medication>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<MedicationStock>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<Sensor>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<SensorReading>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<Alert>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<Request>().HasQueryFilter(e => e.DeletedAt == null);

            base.OnModelCreating(modelBuilder);
        }
    }
}
