using AutoMapper;
using MedFront.Backend.Contracts.DTOs.UpdateDTOs;
using MedFront.Backend.Domain.Entities;

namespace MedFront.Backend.Application.Mappings.UpdatingDTOMappings
{
    public class MedicationUpdateMapping : Profile
    {
        public MedicationUpdateMapping()
        {
            CreateMap<MedicationUpdateDto, Medication>();
        }
    }
}
