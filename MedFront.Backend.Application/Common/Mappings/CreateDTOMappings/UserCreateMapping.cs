using MedFront.Backend.Contracts.DTOs.CreateDTOs;
using MedFront.Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedFront.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class UserCreateMapping : AutoMapper.Profile
    {
        public UserCreateMapping()
        {
            CreateMap<UserCreateDto, User>();
        }
    }
}
