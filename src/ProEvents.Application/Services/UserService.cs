using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProEvents.Application.DTOs.User;
using ProEvents.Application.Interfaces;
using ProEvents.Core.Entities;
using ProEvents.Core.Identity.Model;
using ProEvents.Core.Models;
using ProEvents.Infrastructure.Identity.Service;

namespace ProEvents.Application.Services;
public class UserService : ObjectResult, IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserIdentityService _userIdentityService;
    private readonly IUploadFileService _uploadFileService;
    private const string _imgPath = "resources/images";


    public UserService(IMapper mapper, IUserIdentityService userIdentityService, 
        IUploadFileService uploadFileService)
    {
        _mapper = mapper;
        _userIdentityService = userIdentityService;
        _uploadFileService = uploadFileService;
    }

    public async Task<CustomResult> CreateUserAsync(CreateUserDto userDto)
    {
        var userExists = await _userIdentityService.GetUserByUserNameAsync(userDto.Email);

        if (userExists != null)
        {
            AddError("E-mail already exists");
            return _customResult;
        }

        var user = _mapper.Map<User>(userDto);
        user.UserName = userDto.Email;

        var result = await _userIdentityService.CreateUserAsync(user, userDto.Password);
        if (!result.Succeeded)
        {
            foreach (var err in result.Errors)
                AddError(err.Description);

            return _customResult;
        }

        AddResult(_mapper.Map<UserDto>(user));

        return _customResult;
    }

    public async Task<CustomResult> UpdateUserAsync(string userName, UserUpdateDto userUpdateDto)
    {
        var user = await _userIdentityService.GetUserByUserNameAsync(userName);

        if (user == null)
        {
            AddError("User not found");
            return _customResult;
        }

        user.UserName = user.Email;
        _mapper.Map(userUpdateDto, user);

        var result =  await _userIdentityService.UpdateUserAsync(user);
        if(!result.Succeeded)
        {
            foreach (var err in result.Errors)
                AddError(err.Description);

            return _customResult;
        }

        return _customResult;
    }

    public async Task<CustomResult> CheckUserPasswordAsync(UserLoginDto userDto, string password)
    {
        var user = await _userIdentityService.GetUserByUserNameAsync(userDto.Email);

        if (user == null)
        {
            AddError("User or Password is/are invalid");
            return _customResult;
        }
            

        var result = await _userIdentityService.CheckPasswordSignInAsync(user, password);

        if (!result.Succeeded)
        {
            AddError("User or Password is/are invalid");
            return _customResult;
        }

        AddResult(_mapper.Map<UserDto>(user));

        return _customResult;
    }

    public async Task<UserDto> GetUserByUserNameAsync(string username)
    {
        var user = await _userIdentityService.GetUserByUserNameAsync(username);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<CustomResult> UploadImageAsync(string userName, IFormFile file)
    {
        var user = await _userIdentityService.GetUserByUserNameAsync(userName);

        if (file.Length > 0)
        {
            _uploadFileService.DeleteImage(user.ImageURL, _imgPath);
            user.ImageURL = await _uploadFileService.SaveImage(file, _imgPath);
        }

        await _userIdentityService.UpdateUserAsync(user);

        return _customResult;
    }
}
