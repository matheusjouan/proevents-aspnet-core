using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ProEvents.Application.DTOs.User;
using ProEvents.Core.Models;

namespace ProEvents.Application.Interfaces;
public interface IUserService
{
    Task<CustomResult> CheckUserPasswordAsync(UserLoginDto userDto, string password);
    Task<CustomResult> CreateUserAsync(CreateUserDto userDto);
    Task<CustomResult> UpdateUserAsync(string userName, UserUpdateDto userUpdateDto);
    Task<UserDto> GetUserByUserNameAsync(string username);
    Task<CustomResult> UploadImageAsync(string userName, IFormFile file);
}
