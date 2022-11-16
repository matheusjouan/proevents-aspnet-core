using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProEvents.Application.DTOs.User;
using ProEvents.Application.Interfaces;
using ProEvents.Core.Identity.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProEvents.Application.Services;
public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public TokenService(IConfiguration config, UserManager<User> userManager, IMapper mapper)
    {
        _config = config;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<string> CreateTokenAsync(UserDto userDto)
    {
        var issuer = _config["Jwt:Issuer"];
        var audience = _config["Jwt:Audience"];
        var key = _config["Jwt:Key"];
        var expireInHours = DateTime.Now.AddHours(double.Parse(_config["Jwt:ExpiresInHours"]));

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        var crendentials = new SigningCredentials(
            securityKey, SecurityAlgorithms.HmacSha256
        );

        var user = _mapper.Map<User>(userDto);

        var claims = new List<Claim>
        {
            new Claim("username", userDto.Email),
            new Claim("id", userDto.Id.ToString()),
        };

        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            expires: expireInHours,
            claims: claims,
            signingCredentials: crendentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();

        var stringToken = tokenHandler.WriteToken(token);

        return stringToken;
    }
}
