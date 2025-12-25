using MedFront.Backend.Domain.Common;

namespace MedFront.Backend.Domain.Entities
{
    public class MedicationStock : BaseEntity
    {
        public required Guid MedicationId { get; set; }
        public required virtual Medication Medication { get; set; }

        public required Guid WarehouseId { get; set; }
        public required virtual Warehouse Warehouse { get; set; }

        public int BoxQuantity { get; set; }
        public int StockNorm { get; set; }

        public DateOnly? ExpirationDate { get; set; }

       
    }
}
