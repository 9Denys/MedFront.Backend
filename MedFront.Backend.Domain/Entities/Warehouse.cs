using MedFront.Backend.Domain.Common;

namespace MedFront.Backend.Domain.Entities
{
    public class Warehouse : BaseEntity
    {
        public required string Address { get; set; }

        public decimal TotalVolume { get; set; }
        public decimal CurrentOccupancy { get; set; }

        public virtual ICollection<WarehouseAccess> WarehouseAccesses { get; set; } = new List<WarehouseAccess>();
        public virtual ICollection<MedicationStock> MedicationStocks { get; set; } = new List<MedicationStock>();
        public virtual ICollection<Sensor> Sensors { get; set; } = new List<Sensor>();
        public virtual ICollection<Alert> Alerts { get; set; } = new List<Alert>();

        public virtual ICollection<Request> Requests { get; set; } = new List<Request>();


    }
}
