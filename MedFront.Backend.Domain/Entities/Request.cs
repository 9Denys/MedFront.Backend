using MedFront.Backend.Domain.Common;
using MedFront.Backend.Domain.Enums;

namespace MedFront.Backend.Domain.Entities
{
    public class Request : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }

        public Guid WarehouseId { get; set; }
        public virtual Warehouse? Warehouse { get; set; }

        public Guid MedicationId { get; set; }
        public virtual Medication? Medication { get; set; }

        public RequestStatus RequestStatus { get; set; } = RequestStatus.Created;

        public int BoxQuantity { get; set; }

        public string? Description { get; set; }
    }
}
