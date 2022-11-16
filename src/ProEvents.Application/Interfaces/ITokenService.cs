using ProEvents.Application.DTOs.User;

namespace ProEvents.Application.Interfaces;
public interface ITokenService
{
    Task<string> CreateTokenAsync(UserDto user);
}
