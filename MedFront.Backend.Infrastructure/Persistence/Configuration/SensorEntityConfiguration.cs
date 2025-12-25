using MedFront.Backend.Domain.Entities;
using MedFront.Backend.Domain.Enums;
using MedFront.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedFront.Backend.Infrastructure.Persistence.Configurations
{
    internal class SensorEntityConfiguration : BaseEntityConfiguration<Sensor>
    {
        public override void Configure(EntityTypeBuilder<Sensor> builder)
        {
            base.Configure(builder);

            builder.Property(s => s.SerialName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.SensorType)
                .IsRequired();

            builder.Property(s => s.MinTemperature);
            builder.Property(s => s.MaxTemperature);

            builder.HasMany(s => s.Readings)
                .WithOne(r => r.Sensor)
                .HasForeignKey(r => r.SensorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
