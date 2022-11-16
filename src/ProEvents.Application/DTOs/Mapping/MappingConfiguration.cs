using AutoMapper;
using ProEvents.Application.DTOs.Batchs;
using ProEvents.Application.DTOs.Events;
using ProEvents.Application.DTOs.SocialNetworks;
using ProEvents.Application.DTOs.Speakers;
using ProEvents.Application.DTOs.User;
using ProEvents.Core.Entities;
using ProEvents.Core.Models;
using UserX = ProEvents.Core.Identity.Model;

namespace ProEvents.Application.DTOs.Mapping;
public class MappingConfiguration : Profile
{
	public MappingConfiguration()
	{
		CreateMap<EventDto, Event>().ReverseMap();
        CreateMap<CreateEventDto, Event>().ReverseMap();
        CreateMap<UpdateEventDto, Event>().ReverseMap();

        CreateMap<BatchDto, Batch>().ReverseMap();
        CreateMap<SaveBatchDto, Batch>().ReverseMap();

        CreateMap<SpeakerDto, Speaker>().ReverseMap();
        CreateMap<CreateSpeakerDto, Speaker>().ReverseMap();
        CreateMap<UpdateSpeakerDto, Speaker>().ReverseMap();

        CreateMap<SocialNetworkDto, SocialNetwork>().ReverseMap();
        CreateMap<SaveSocialNetworkDto, SocialNetwork>().ReverseMap();

        CreateMap<UserDto, UserX.User>().ReverseMap();
        CreateMap<CreateUserDto, UserX.User>().ReverseMap();
        CreateMap<UserUpdateDto, UserX.User>().ReverseMap();
        CreateMap<UserLoginDto, UserX.User>().ReverseMap();


    }
}
