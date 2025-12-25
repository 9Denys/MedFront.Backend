using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedFront.Backend.Contracts.DTOs.Enums;

namespace MedFront.Backend.Contracts.DTOs.ReadingDTOs
{
    public class SensorDto
    {
        public Guid Id { get; set; }

        public Guid WarehouseId { get; set; }
        public string? WarehouseAddress { get; set; }

        public string SerialName { get; set; } = default!;

        public SensorType SensorType { get; set; }

        public decimal? MinTemperature { get; set; }
        public decimal? MaxTemperature { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
