using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedFront.Backend.Contracts.DTOs.ReadingDTOs
{
    public class AlertDto
    {
        public Guid Id { get; set; }

        public Guid WarehouseId { get; set; }

        public Guid? MedicationId { get; set; }
        public string? MedicationName { get; set; }

        public string Message { get; set; } = null!;

        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ReadAt { get; set; }
    }
}
