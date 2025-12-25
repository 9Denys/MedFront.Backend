using AutoMapper;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MedFront.Backend.Domain.Entities;

namespace MedFront.Backend.Application.Mappings.ReadingDTOMappings
{
    public class WarehouseGetMapping : Profile
    {
        public WarehouseGetMapping()
        {
            CreateMap<Warehouse, WarehouseDto>();
        }
    }
}
