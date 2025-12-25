using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedFront.Backend.Contracts.DTOs.CreateDTOs
{
    public class CreateRequestDto
    {
        public Guid WarehouseId { get; set; }

        public Guid MedicationId { get; set; }

        public int BoxQuantity { get; set; }

        public string? Description { get; set; }
    }
}
