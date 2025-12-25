using MedFront.Backend.Domain.Entities;
using MedFront.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedFront.Backend.Infrastructure.Persistence.Configurations
{
    internal class SensorReadingEntityConfiguration : BaseEntityConfiguration<SensorReading>
    {
        public override void Configure(EntityTypeBuilder<SensorReading> builder)
        {
            base.Configure(builder);

            builder.Property(sr => sr.Time)
                .IsRequired();

            builder.Property(sr => sr.Value)
                .IsRequired();

            builder.HasIndex(sr => new { sr.SensorId, sr.Time });
        }
    }
}
