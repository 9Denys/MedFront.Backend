using AutoMapper;
using MedFront.Backend.Contracts.DTOs.UpdateDTOs;
using MedFront.Backend.Domain.Entities;

namespace MedFront.Backend.Application.Mappings.UpdatingDTOMappings
{
    public class WarehouseUpdateMapping : Profile
    {
        public WarehouseUpdateMapping()
        {
            CreateMap<WarehouseUpdateDto, Warehouse>()
                .ForMember(dest => dest.CurrentOccupancy, opt => opt.Ignore());
        }
    }
}
