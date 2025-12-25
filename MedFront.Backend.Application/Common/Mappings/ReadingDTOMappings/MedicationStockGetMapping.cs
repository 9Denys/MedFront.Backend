using AutoMapper;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MedFront.Backend.Domain.Entities;

namespace MedFront.Backend.Application.Mappings.ReadingDTOMappings
{
    public class MedicationStockGetMapping : Profile
    {
        public MedicationStockGetMapping()
        {
            CreateMap<MedicationStock, MedicationStockDto>()
                .ForMember(d => d.StockId, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.MedicationName, opt => opt.MapFrom(s => s.Medication.Name));
        }
    }
}
