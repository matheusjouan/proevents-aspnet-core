using Microsoft.AspNetCore.Identity;

namespace ProEvents.Core.Identity.Model;
public class Role : IdentityRole<long>
{
    public IEnumerable<UserRole> UserRoles { get; set; }
}
