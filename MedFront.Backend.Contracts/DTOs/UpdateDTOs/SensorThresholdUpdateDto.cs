using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedFront.Backend.Contracts.DTOs.UpdateDTOs
{
    public class SensorThresholdUpdateDto
    {
        public decimal? MinTemperature { get; set; }
        public decimal? MaxTemperature { get; set; }
    }
}
