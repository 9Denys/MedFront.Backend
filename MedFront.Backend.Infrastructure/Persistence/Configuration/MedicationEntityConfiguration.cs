using MedFront.Backend.Domain.Entities;
using MedFront.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedFront.Backend.Infrastructure.Persistence.Configurations
{
    internal class MedicationEntityConfiguration : BaseEntityConfiguration<Medication>
    {
        public override void Configure(EntityTypeBuilder<Medication> builder)
        {
            base.Configure(builder);

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.SKU)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(m => m.SKU)
                .IsUnique();

            builder.Property(m => m.Category)
                .HasMaxLength(50);

            builder.Property(m => m.PackageVolume)
                .IsRequired();

            builder.HasMany(m => m.MedicationStocks)
                .WithOne(ms => ms.Medication)
                .HasForeignKey(ms => ms.MedicationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.Alerts)
                .WithOne(a => a.Medication)
                .HasForeignKey(a => a.MedicationId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
