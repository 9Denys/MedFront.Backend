using AutoMapper;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MedFront.Backend.Domain.Entities;

namespace MedFront.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class SensorReadingGetMapping : Profile
    {
        public SensorReadingGetMapping()
        {
            CreateMap<SensorReading, SensorReadingDto>();
        }
    }
}
