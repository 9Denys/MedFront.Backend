using MedFront.Backend.Domain.Entities;
using MedFront.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedFront.Backend.Infrastructure.Persistence.Configurations
{
    internal class RequestEntityConfiguration : BaseEntityConfiguration<Request>
    {
        public override void Configure(EntityTypeBuilder<Request> builder)
        {
            base.Configure(builder);

            builder.Property(r => r.BoxQuantity)
                .IsRequired();

            builder.Property(r => r.RequestStatus)
                .IsRequired()
                .HasConversion<int>(); // enum -> int

            builder.Property(r => r.Description)
                .HasMaxLength(1000); // можешь убрать лимит, если не хочешь

            builder.HasIndex(r => r.UserId);
            builder.HasIndex(r => r.WarehouseId);
            builder.HasIndex(r => r.MedicationId);

            // удобно для админки: быстро "последние заявки по складу"
            builder.HasIndex(r => new { r.WarehouseId, r.CreatedAt });

            // удобно для поиска по медикаменту + дата
            builder.HasIndex(r => new { r.MedicationId, r.CreatedAt });

            builder.HasOne(r => r.User)
                .WithMany(u => u.Requests)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Warehouse)
                .WithMany(w => w.Requests)
                .HasForeignKey(r => r.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Medication)
                .WithMany(m => m.Requests)
                .HasForeignKey(r => r.MedicationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
