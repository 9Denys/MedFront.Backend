using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedFront.Backend.Contracts.DTOs.Enums;

namespace MedFront.Backend.Contracts.DTOs.AuthDTOs
{
    public class UserInfoDto
    {
        public Guid Id { get; set; }
        public Role Role { get; set; }
    }
}
