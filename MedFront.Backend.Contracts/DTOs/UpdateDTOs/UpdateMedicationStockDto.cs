using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedFront.Backend.Contracts.DTOs.UpdateDTOs
{
    public class UpdateMedicationStockDto
    {
        public int BoxQuantity { get; set; }
        public int StockNorm { get; set; }
        public DateOnly? ExpirationDate { get; set; }
    }
}

