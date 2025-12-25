using System;

namespace MedFront.Backend.Contracts.DTOs.ReadingDTOs
{
    public class MedicationStockDto
    {
        public Guid StockId { get; set; }

        public Guid MedicationId { get; set; }
        public string? MedicationName { get; set; }

        public Guid WarehouseId { get; set; }

        public int BoxQuantity { get; set; }
        public int StockNorm { get; set; }
        public DateOnly? ExpirationDate { get; set; }
    }
}
