using AutoMapper;
using MedFront.Backend.Contracts.DTOs.CreateDTOs;
using MedFront.Backend.Domain.Entities;
using MedFront.Backend.Domain.Enums;

namespace MedFront.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class RequestCreateMapping : Profile
    {
        public RequestCreateMapping()
        {
            CreateMap<CreateRequestDto, Request>()
                .ForMember(d => d.RequestStatus, o => o.MapFrom(_ => RequestStatus.Created));
        }
    }
}
