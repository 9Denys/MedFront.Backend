using AutoMapper;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MedFront.Backend.Domain.Entities;

namespace MedFront.Backend.Application.Mappings.ReadingDTOMappings
{
    public class MedicationGetMapping : Profile
    {
        public MedicationGetMapping()
        {
            CreateMap<Medication, MedicationDto>();
        }
    }
}
