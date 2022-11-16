using ProEvents.Application.DTOs.Events;
using ProEvents.Application.DTOs.Speakers;
using ProEvents.Core.Entities;

namespace ProEvents.Application.DTOs.SocialNetworks;
public class SocialNetworkDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string URL { get; set; }
    public long? EventId { get; set; }
    public EventDto Event { get; set; }
    public long? SpeakerId { get; set; }
    public SpeakerDto Speaker { get; set; }
}
