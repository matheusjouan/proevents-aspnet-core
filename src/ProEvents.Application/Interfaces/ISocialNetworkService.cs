using ProEvents.Application.DTOs.SocialNetworks;
using ProEvents.Core.Models;

namespace ProEvents.Application.Interfaces;
public interface ISocialNetworkService
{
    Task<CustomResult> SaveSocialNetworkByEventAsync(long eventId, IEnumerable<SaveSocialNetworkDto> models);
    Task<CustomResult> DeleteSocialNetworkByEventAsync(long id, long eventId);
    Task<CustomResult> GetAllSocialNetworkByEventAsync(long eventId);
    Task<CustomResult> GetSocialNetworkByEventAsync(long id, long eventId);

    Task<CustomResult> SaveSocialNetworkBySpeakerAsync(long speakerId, IEnumerable<SaveSocialNetworkDto> models);
    Task<CustomResult> DeleteSocialNetworkBySpeakerAsync(long id, long speakerId);
    Task<CustomResult> GetAllSocialNetworkBySpeakerAsync(long speakerId);
    Task<CustomResult> GetSocialNetworkBySpeakerAsync(long id, long speakerId);

}
