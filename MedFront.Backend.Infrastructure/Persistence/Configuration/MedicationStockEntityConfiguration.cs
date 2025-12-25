using MedFront.Backend.Domain.Entities;
using MedFront.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedFront.Backend.Infrastructure.Persistence.Configurations
{
    internal class MedicationStockEntityConfiguration : BaseEntityConfiguration<MedicationStock>
    {
        public override void Configure(EntityTypeBuilder<MedicationStock> builder)
        {
            base.Configure(builder);

            builder.Property(ms => ms.BoxQuantity)
                .IsRequired();

            builder.Property(ms => ms.StockNorm)
                .IsRequired();

            builder.Property(ms => ms.ExpirationDate);
        }
    }
}
