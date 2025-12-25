using MedFront.Backend.Domain.Common;

namespace MedFront.Backend.Domain.Entities
{
    public class Alert : BaseEntity
    {
        public required Guid WarehouseId { get; set; }

        public virtual Warehouse Warehouse { get; set; } = null!;

        public Guid? MedicationId { get; set; }
        public virtual Medication? Medication { get; set; }

        public required string Message { get; set; }

        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
    }
}
