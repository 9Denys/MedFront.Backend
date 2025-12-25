using MedFront.Backend.Domain.Entities;
using MedFront.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedFront.Backend.Infrastructure.Persistence.Configurations
{
    internal class WarehouseEntityConfiguration : BaseEntityConfiguration<Warehouse>
    {
        public override void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            base.Configure(builder);

            builder.Property(w => w.Address)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(w => w.TotalVolume)
                .IsRequired();

            builder.Property(w => w.CurrentOccupancy)
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasMany(w => w.MedicationStocks)
                .WithOne(ms => ms.Warehouse)
                .HasForeignKey(ms => ms.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(w => w.Sensors)
                .WithOne(s => s.Warehouse)
                .HasForeignKey(s => s.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(w => w.Alerts)
                .WithOne(a => a.Warehouse)
                .HasForeignKey(a => a.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(w => w.WarehouseAccesses)
                .WithOne(wa => wa.Warehouse)
                .HasForeignKey(wa => wa.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
