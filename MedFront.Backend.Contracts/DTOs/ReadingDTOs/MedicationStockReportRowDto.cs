using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedFront.Backend.Contracts.DTOs.ReadingDTOs
{
    public class MedicationStockReportRowDto
    {
        public string MedicationName { get; set; } = default!;
        public string WarehouseAddress { get; set; } = default!;
        public int BoxQuantity { get; set; }
        public int StockNorm { get; set; }
        public string? ExpirationDate { get; set; } 
    }
}
