using AutoMapper;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MedFront.Backend.Domain.Entities;
using MedFront.Backend.Contracts.DTOs.Enums;

namespace MedFront.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class RequestGetMapping : Profile
    {
        public RequestGetMapping()
        {
            CreateMap<Request, RequestDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.WarehouseAddress, o => o.MapFrom(s => s.Warehouse != null ? s.Warehouse.Address : null))
                .ForMember(d => d.MedicationName, o => o.MapFrom(s => s.Medication != null ? s.Medication.Name : null));
        }
    }
}
