using AutoMapper;
using MedFront.Backend.Contracts.DTOs.UpdateDTOs;
using MedFront.Backend.Domain.Entities;

namespace MedFront.Backend.Application.Common.Mappings.UpdatingDTOMappings
{
    public class SensorThresholdUpdateMapping : Profile
    {
        public SensorThresholdUpdateMapping()
        {
            CreateMap<SensorThresholdUpdateDto, Sensor>()
                .ForMember(d => d.MinTemperature, o => o.MapFrom(s => s.MinTemperature))
                .ForMember(d => d.MaxTemperature, o => o.MapFrom(s => s.MaxTemperature));
        }
    }
}
