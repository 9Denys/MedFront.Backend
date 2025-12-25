using System;

namespace MedFront.Backend.Contracts.DTOs.UpdateDTOs
{
    public class WarehouseUpdateDto
    {
        public string Address { get; set; } = default!;
        public decimal TotalVolume { get; set; }
    }
}
