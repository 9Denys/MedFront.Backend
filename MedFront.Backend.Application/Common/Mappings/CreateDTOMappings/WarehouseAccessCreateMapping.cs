using AutoMapper;
using MedFront.Backend.Contracts.DTOs.CreateDTOs;
using MedFront.Backend.Domain.Entities;

namespace MedFront.Backend.Application.Mappings.CreateDTOMappings
{
    public class WarehouseAccessCreateMapping : Profile
    {
        public WarehouseAccessCreateMapping()
        {
            CreateMap<WarehouseAccessCreateDto, WarehouseAccess>()
                .ForMember(d => d.User, opt => opt.Ignore())
                .ForMember(d => d.Warehouse, opt => opt.Ignore());
        }
    }
}
