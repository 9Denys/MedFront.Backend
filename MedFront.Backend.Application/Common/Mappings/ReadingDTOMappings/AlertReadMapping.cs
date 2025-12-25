using AutoMapper;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MedFront.Backend.Domain.Entities;

namespace MedFront.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class AlertReadMapping : Profile
    {
        public AlertReadMapping()
        {
            CreateMap<Alert, AlertDto>()
                .ForMember(d => d.MedicationName,
                    opt => opt.MapFrom(s => s.Medication != null ? s.Medication.Name : null));
        }
    }
}
