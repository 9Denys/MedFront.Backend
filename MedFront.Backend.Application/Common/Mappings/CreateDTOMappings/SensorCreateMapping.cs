using AutoMapper;
using MedFront.Backend.Contracts.DTOs.CreateDTOs;
using MedFront.Backend.Domain.Entities;
using DomainSensorType = MedFront.Backend.Domain.Enums.SensorType;

namespace MedFront.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class SensorCreateMapping : Profile
    {
        public SensorCreateMapping()
        {
            CreateMap<SensorCreateDto, Sensor>()
                .ForMember(d => d.SensorType, o => o.MapFrom(s => (DomainSensorType)(int)s.SensorType));
        }
    }
}
