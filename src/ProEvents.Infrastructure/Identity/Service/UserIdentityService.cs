using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProEvents.Core.Identity.Model;

namespace ProEvents.Infrastructure.Identity.Service;
public class UserIdentityService : IUserIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserIdentityService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<SignInResult> CheckPasswordSignInAsync(User user, string password)
    {
        return await _signInManager.CheckPasswordSignInAsync(user, password, false);
    }

    public async Task<IdentityResult> CreateUserAsync(User user, string password)
    {
        return  await _userManager.CreateAsync(user, password);
    }

    public async Task<IdentityResult> UpdateUserAsync(User user)
    {
        //var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //var result = await _userManager.ResetPasswordAsync(user, token, user.PasswordHash);
        return await _userManager.UpdateAsync(user);
    }

    public async Task<User> GetUserByUserNameAsync(string userName)
    {
        return await _userManager.Users
            .FirstOrDefaultAsync(u => u.UserName == userName);
    }
}
