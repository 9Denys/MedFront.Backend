using System;

namespace MedFront.Backend.Contracts.DTOs.ReadingDTOs
{
    public class WarehouseAccessDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid WarehouseId { get; set; }
    }
}
