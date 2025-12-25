using System;

namespace MedFront.Backend.Contracts.DTOs.ReadingDTOs
{
    public class WarehouseDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; } = default!;
        public decimal TotalVolume { get; set; }
        public decimal CurrentOccupancy { get; set; }
    }
}
