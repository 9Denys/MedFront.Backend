using System;

namespace MedFront.Backend.Contracts.DTOs.CreateDTOs
{
    public class WarehouseAccessCreateDto
    {
        public Guid UserId { get; set; }
        public Guid WarehouseId { get; set; }
    }
}
