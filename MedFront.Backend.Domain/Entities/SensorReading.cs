using MedFront.Backend.Domain.Common;

namespace MedFront.Backend.Domain.Entities
{
    public class SensorReading : BaseEntity
    {
        public required Guid SensorId { get; set; }
        public required virtual Sensor Sensor { get; set; }

        public DateTime Time { get; set; } = DateTime.UtcNow;
        public decimal Value { get; set; }
    }
}
