using ProEvents.Core.Entities;

namespace ProEvents.Core.Interface;
public interface ISocialNetworkRepository : IBaseRepository<SocialNetwork>
{
    Task<SocialNetwork> GetSocialNetworkEventByIdsAsync(long eventId, long id);
    Task<SocialNetwork> GetSocialNetworkSpeakerByIdsAsync(long speakerId, long id);
    Task<IEnumerable<SocialNetwork>> GetAllSocialNetworkByEventAsync(long eventId);
    Task<IEnumerable<SocialNetwork>> GetAllSocialNetworkSpeakersAsync(long speakerId);
}
