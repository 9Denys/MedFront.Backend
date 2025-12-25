using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedFront.Backend.Contracts.DTOs.MedicationStock
{
    public class CreateMedicationStockDto
    {
        public Guid MedicationId { get; set; }
        public Guid WarehouseId { get; set; }

        public int BoxQuantity { get; set; }
        public int StockNorm { get; set; }

        public DateOnly? ExpirationDate { get; set; }
    }
}

