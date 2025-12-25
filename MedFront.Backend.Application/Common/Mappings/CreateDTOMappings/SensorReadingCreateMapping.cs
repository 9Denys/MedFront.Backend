using AutoMapper;
using MedFront.Backend.Contracts.DTOs.CreateDTOs;
using MedFront.Backend.Domain.Entities;

namespace MedFront.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class SensorReadingCreateMapping : Profile
    {
        public SensorReadingCreateMapping()
        {
            CreateMap<SensorReadingCreateDto, SensorReading>()
                .ForMember(d => d.Time, o => o.Ignore());
        }
    }
}
