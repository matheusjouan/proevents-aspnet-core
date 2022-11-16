using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProEvents.Core.Identity.Model;
using ProEvents.Core.Interface;
using ProEvents.Infrastructure.Identity.Service;
using ProEvents.Infrastructure.Persistence.Context;
using ProEvents.Infrastructure.Persistence.Repositories;
using ProEvents.Infrastructure.Persistence.UnitOfWork;
using ProSpeakers.Core.Interface;
using System.Text;

namespace ProEvents.Infrastructure.Extensions;
public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");

        services.AddDbContext<ProEventsContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IBatchRepository, BatchRepository>();
        services.AddScoped<ISpeakerRepository, SpeakerRepository>();
        services.AddScoped<ISocialNetworkRepository, SocialNetworkRepository>();
        services.AddScoped<IUserIdentityService, UserIdentityService>();

        return services;
    }

    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<User>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 4;
        })
        .AddRoles<Role>()
        .AddRoleManager<RoleManager<Role>>()
        .AddSignInManager<SignInManager<User>>()
        .AddUserManager<UserManager<User>>()
        .AddRoleValidator<RoleValidator<Role>>()
        .AddEntityFrameworkStores<ProEventsContext>()
        .AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(
            JwtBearerDefaults.AuthenticationScheme
        ).AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(config["Jwt:Key"])
                ),
                // Caso for usado Issuer e Audience na aplicação
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = config["Jwt:Audience"],
                ValidIssuer = config["Jwt:Issuer"],
            }
        );

        return services;
    }
}
