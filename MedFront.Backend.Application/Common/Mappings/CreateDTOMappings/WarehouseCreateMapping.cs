using AutoMapper;
using MedFront.Backend.Contracts.DTOs.CreateDTOs;
using MedFront.Backend.Domain.Entities;

namespace MedFront.Backend.Application.Mappings.CreateDTOMappings
{
    public class WarehouseCreateMapping : Profile
    {
        public WarehouseCreateMapping()
        {
            CreateMap<WarehouseCreateDto, Warehouse>()
                .ForMember(dest => dest.CurrentOccupancy, opt => opt.MapFrom(_ => 0));
        }
    }
}
