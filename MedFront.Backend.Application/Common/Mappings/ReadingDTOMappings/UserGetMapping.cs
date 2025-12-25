using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MedFront.Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedFront.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class UserGetMapping : AutoMapper.Profile
    {
        public UserGetMapping()
        {
            CreateMap<User, UserDto>();
        }
    }
}
