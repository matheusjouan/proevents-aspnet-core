using Microsoft.AspNetCore.Identity;
using ProEvents.Core.Identity.Model;

namespace ProEvents.Infrastructure.Identity.Service;
public interface IUserIdentityService
{
    Task<IdentityResult> CreateUserAsync(User user, string password);
    Task<IdentityResult> UpdateUserAsync(User user);
    Task<User> GetUserByUserNameAsync(string username);
    Task<SignInResult> CheckPasswordSignInAsync(User user, string password);
}
