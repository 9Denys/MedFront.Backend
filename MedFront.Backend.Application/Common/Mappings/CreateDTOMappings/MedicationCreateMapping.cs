using AutoMapper;
using MedFront.Backend.Contracts.DTOs.CreateDTOs;
using MedFront.Backend.Domain.Entities;

namespace MedFront.Backend.Application.Mappings.CreateDTOMappings
{
    public class MedicationCreateMapping : Profile
    {
        public MedicationCreateMapping()
        {
            CreateMap<MedicationCreateDto, Medication>();
        }
    }
}
