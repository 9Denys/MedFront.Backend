using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedFront.Backend.Contracts.DTOs.CreateDTOs
{
    public class SensorReadingCreateDto
    {
        public Guid SensorId { get; set; }

        public DateTime? Time { get; set; }

        public decimal Value { get; set; }
    }
}
