using Microsoft.AspNetCore.Identity;
using ProEvents.Core.Identity.Enum;

namespace ProEvents.Core.Identity.Model;
public class User : IdentityUser<long>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Title Title { get; set; }
    public string Description { get; set; }
    public UserType Type { get; set; }
    public string ImageURL { get; set; }
    public IEnumerable<UserRole> UserRoles { get; set; }
}
