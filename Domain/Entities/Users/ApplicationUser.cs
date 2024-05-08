using Domain.Attributes;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Users
{
    [Auditable]
    public class ApplicationUser : IdentityUser<Guid>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime InsertTime { get; set; }
        public bool IsAdmin { get; set; }
    }
}