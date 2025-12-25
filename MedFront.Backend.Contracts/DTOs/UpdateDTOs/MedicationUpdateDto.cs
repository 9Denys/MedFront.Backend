using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedFront.Backend.Contracts.DTOs.UpdateDTOs
{
    public class MedicationUpdateDto
    {
        public string Name { get; set; } = default!;
        public string? Details { get; set; }
        public string SKU { get; set; } = default!;
        public string? Category { get; set; }
        public decimal PackageVolume { get; set; }
    }
}
