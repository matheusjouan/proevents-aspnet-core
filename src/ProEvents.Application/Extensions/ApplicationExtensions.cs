using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ProEvents.Application.DTOs.Mapping;
using ProEvents.Application.Interfaces;
using ProEvents.Application.Services;

namespace ProEvents.Application.Extensions;
public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingConfiguration());
        });

        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);

        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IBatchService, BatchService>();
        services.AddScoped<ISpeakerService, SpeakerService>();
        services.AddScoped<ISocialNetworkService, SocialNetworkService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUploadFileService, UploadFileService>();

        return services;
    }
}
