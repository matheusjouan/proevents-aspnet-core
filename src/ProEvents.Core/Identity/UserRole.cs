using Microsoft.AspNetCore.Identity;

namespace ProEvents.Core.Identity.Model;
public class UserRole : IdentityUserRole<long>
{
    public User User { get; set; }
    public Role Role { get; set; }
}
