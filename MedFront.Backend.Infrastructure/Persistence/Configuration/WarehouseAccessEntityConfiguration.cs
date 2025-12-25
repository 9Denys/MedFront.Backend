using MedFront.Backend.Domain.Entities;
using MedFront.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedFront.Backend.Infrastructure.Persistence.Configurations
{
    internal class WarehouseAccessEntityConfiguration : BaseEntityConfiguration<WarehouseAccess>
    {
        public override void Configure(EntityTypeBuilder<WarehouseAccess> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.User)
                .WithMany(u => u.WarehouseAccesses)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Warehouse)
                .WithMany(w => w.WarehouseAccesses)
                .HasForeignKey(x => x.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.UserId, x.WarehouseId })
                .IsUnique();
        }
    }
}
