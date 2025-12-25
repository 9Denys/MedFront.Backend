using MedFront.Backend.Domain.Common;
using MedFront.Backend.Domain.Enums;

namespace MedFront.Backend.Domain.Entities
{
    public class Sensor : BaseEntity
    {
        public required Guid WarehouseId { get; set; }
        public required virtual Warehouse Warehouse { get; set; }

        public required string SerialName { get; set; }

        public SensorType SensorType { get; set; }

        public decimal? MinTemperature { get; set; }
        public decimal? MaxTemperature { get; set; }

        public virtual ICollection<SensorReading> Readings { get; set; } = new List<SensorReading>();
    }
}
