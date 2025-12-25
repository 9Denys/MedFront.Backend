using System;

namespace MedFront.Backend.Contracts.DTOs.CreateDTOs
{
    public class WarehouseCreateDto
    {
        public string Address { get; set; } = default!;
        public decimal TotalVolume { get; set; }
    }
}
