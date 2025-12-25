using AutoMapper;
using MedFront.Backend.Contracts.DTOs.UpdateDTOs;
using MedFront.Backend.Domain.Entities;

namespace MedFront.Backend.Application.Mappings.UpdatingDTOMappings
{
    public class MedicationStockUpdateMapping : Profile
    {
        public MedicationStockUpdateMapping()
        {
            CreateMap<UpdateMedicationStockDto, MedicationStock>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.MedicationId, opt => opt.Ignore())
                .ForMember(d => d.WarehouseId, opt => opt.Ignore())
                .ForMember(d => d.Medication, opt => opt.Ignore())
                .ForMember(d => d.Warehouse, opt => opt.Ignore());
     
        }
    }
}
