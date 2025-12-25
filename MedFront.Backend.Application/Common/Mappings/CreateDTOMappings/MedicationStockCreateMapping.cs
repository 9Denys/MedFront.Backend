using AutoMapper;
using MedFront.Backend.Contracts.DTOs.CreateDTOs;
using MedFront.Backend.Contracts.DTOs.MedicationStock;
using MedFront.Backend.Domain.Entities;

namespace MedFront.Backend.Application.Mappings.CreateDTOMappings
{
    public class MedicationStockCreateMapping : Profile
    {
        public MedicationStockCreateMapping()
        {
            CreateMap<CreateMedicationStockDto, MedicationStock>()
                .ForMember(d => d.Medication, opt => opt.Ignore())
                .ForMember(d => d.Warehouse, opt => opt.Ignore());
            
        }
    }
}
