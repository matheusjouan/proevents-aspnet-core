using ProEvents.Application.DTOs.Batchs;
using ProEvents.Application.DTOs.SocialNetworks;
using ProEvents.Application.DTOs.Speakers;

namespace ProEvents.Application.DTOs.Events;
public class EventDto
{
    public long Id { get; set; }
    public string Place { get; set; }
    public DateTime? EventDate { get; set; }
    public string Theme { get; set; }
    public int AmountOfPeople { get; set; }
    public string ImageURL { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public IEnumerable<BatchDto> Batches { get; set; }
    public IEnumerable<SpeakerDto> Speakers { get; set; }
    public IEnumerable<SocialNetworkDto> SocialNetworks { get; set; }
}
