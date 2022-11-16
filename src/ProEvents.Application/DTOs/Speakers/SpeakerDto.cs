using ProEvents.Application.DTOs.Events;
using ProEvents.Application.DTOs.SocialNetworks;
using ProEvents.Application.DTOs.User;

namespace ProEvents.Application.DTOs.Speakers;
public class SpeakerDto
{
    public long Id { get; set; }
    public string Resume { get; set; }
    public long UserId { get; set; }
    public UserDto User { get; set; }
    public IEnumerable<SocialNetworkDto> SocialNetworks { get; set; }
    public IEnumerable<EventDto> Events { get; set; }
}
