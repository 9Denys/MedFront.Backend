using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedFront.Backend.Contracts.DTOs.ReadingDTOs
{
    public class SensorReadingQueryDto
    {
        public DateTime? FromUtc { get; set; }
        public DateTime? ToUtc { get; set; }

        public int? Take { get; set; }
    }
}
