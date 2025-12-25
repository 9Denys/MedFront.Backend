using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedFront.Backend.Contracts.DTOs.ReadingDTOs
{
    public class SensorReadingDto
    {
        public Guid Id { get; set; }

        public Guid SensorId { get; set; }

        public DateTime Time { get; set; }
        public decimal Value { get; set; }
    }
}

