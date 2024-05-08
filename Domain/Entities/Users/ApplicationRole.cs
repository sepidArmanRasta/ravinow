using Domain.Attributes;
using Domain.Enumeration.Users;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Users
{
    [Auditable]
    public class ApplicationRole : IdentityRole<Guid>
    {
        public int AccessCode { get; set; }
        public AccessTypeEnum AccessType { get; set; }
    }
}