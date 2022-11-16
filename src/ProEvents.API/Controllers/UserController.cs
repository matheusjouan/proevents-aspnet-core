using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEvents.API.Extensions;
using ProEvents.Application.DTOs.User;
using ProEvents.Application.Interfaces;
using ProEvents.Core.Models;

namespace ProEvents.API.Controllers;

[Authorize]
[Route("api/[controller]")]
 public class UserController : MainController
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public UserController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetUserAsync()
    {
        var username = User.GetUserName();

        var user = await _userService.GetUserByUserNameAsync(username);
        return Ok(user);
    }

    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<IActionResult> CreateUserAsync(CreateUserDto request)
    {
        var result = await _userService.CreateUserAsync(request);

        return CustomResponse(result);
    }

    [AllowAnonymous]
    [HttpPost("signin")]
    public async Task<IActionResult> SignInAsync(UserLoginDto request)
    {
        var result = await _userService.CheckUserPasswordAsync(request, request.Password);

        if (!result.IsValid)
            return CustomResponse(result);

        UserDto user = (UserDto)result.Result;
        var token = await _tokenService.CreateTokenAsync(user);

        return CustomResponse(new
        {
            Id = user.Id,
            UserName = user.Email,
            FirstName = user.FirstName,
            Token = token,
        });
    }

    [HttpPut()]
    public async Task<IActionResult> UpdateUserAsync(UserUpdateDto request)
    {
        var username = User.GetUserName();

        var result = await _userService.UpdateUserAsync(username, request);

        if(!result.IsValid)
            return NotFound(result);

        return NoContent();
    }

    [HttpPost("profile-img")]
    public async Task<IActionResult> UploadFileImageAsync()
    {
        var username = User.GetUserName();
        var file = Request.Form.Files[0];

        await _userService.UploadImageAsync(username, file);

        return NoContent();
    }
}
