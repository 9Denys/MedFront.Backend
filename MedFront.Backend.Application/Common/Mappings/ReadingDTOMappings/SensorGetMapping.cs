using AutoMapper;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MedFront.Backend.Domain.Entities;
using ContractsSensorType = MedFront.Backend.Contracts.DTOs.Enums.SensorType;

namespace MedFront.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class SensorGetMapping : Profile
    {
        public SensorGetMapping()
        {
            CreateMap<Sensor, SensorDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.WarehouseAddress, o => o.MapFrom(s => s.Warehouse != null ? s.Warehouse.Address : null))
                .ForMember(d => d.SensorType, o => o.MapFrom(s => (ContractsSensorType)(int)s.SensorType));
        }
    }
}
