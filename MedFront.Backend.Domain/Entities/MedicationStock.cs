using MedFront.Backend.Domain.Common;

namespace MedFront.Backend.Domain.Entities
{
    public class MedicationStock : BaseEntity
    {
        public Guid MedicationId { get; set; }
        public virtual Medication? Medication { get; set; }

        public Guid WarehouseId { get; set; }
        public virtual Warehouse? Warehouse { get; set; }

        public int BoxQuantity { get; set; }
        public int StockNorm { get; set; }
        public DateOnly? ExpirationDate { get; set; }
    }

}
