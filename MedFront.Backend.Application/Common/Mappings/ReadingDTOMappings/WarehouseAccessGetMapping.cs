using AutoMapper;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MedFront.Backend.Domain.Entities;

namespace MedFront.Backend.Application.Mappings.ReadingDTOMappings
{
    public class WarehouseAccessGetMapping : Profile
    {
        public WarehouseAccessGetMapping()
        {
            CreateMap<WarehouseAccess, WarehouseAccessDto>();
        }
    }
}
