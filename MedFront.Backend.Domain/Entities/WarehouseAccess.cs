using MedFront.Backend.Domain.Common;

namespace MedFront.Backend.Domain.Entities
{
    public class WarehouseAccess : BaseEntity
    {
        public required Guid UserId { get; set; }
        public required virtual User User { get; set; }

        public required Guid WarehouseId { get; set; }
        public required virtual Warehouse Warehouse { get; set; }
    }
}
