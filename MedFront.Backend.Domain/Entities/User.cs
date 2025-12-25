using MedFront.Backend.Domain.Common;
using MedFront.Backend.Domain.Enums;

namespace MedFront.Backend.Domain.Entities
{
    public class User : BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }

        public Role Role { get; set; } = Role.Medic;
        public required string PasswordHash { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTime? LastLoginAt { get; set; }


        public virtual ICollection<WarehouseAccess> WarehouseAccesses { get; set; } = new List<WarehouseAccess>();
        public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
    }
}
