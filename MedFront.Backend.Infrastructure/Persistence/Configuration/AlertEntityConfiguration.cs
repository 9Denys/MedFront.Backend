using MedFront.Backend.Domain.Entities;
using MedFront.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedFront.Backend.Infrastructure.Persistence.Configurations
{
    internal class AlertEntityConfiguration : BaseEntityConfiguration<Alert>
    {
        public override void Configure(EntityTypeBuilder<Alert> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Message)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(a => a.IsRead)
                .IsRequired();

            builder.HasOne(a => a.Warehouse)
                .WithMany(w => w.Alerts)
                .HasForeignKey(a => a.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
