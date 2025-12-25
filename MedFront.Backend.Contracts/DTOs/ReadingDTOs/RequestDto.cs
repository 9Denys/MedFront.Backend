using MedFront.Backend.Contracts.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedFront.Backend.Contracts.DTOs.ReadingDTOs
{
    public class RequestDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid WarehouseId { get; set; }
        public string? WarehouseAddress { get; set; }

        public Guid MedicationId { get; set; }
        public string? MedicationName { get; set; }

        public int BoxQuantity { get; set; }

        public RequestStatus RequestStatus { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
