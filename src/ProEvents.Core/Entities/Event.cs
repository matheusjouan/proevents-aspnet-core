using ProEvents.Core.Identity.Model;

namespace ProEvents.Core.Entities;
public class Event : EntityBase
{
    public string Place { get; set; }
    public DateTime? EventDate { get; set; }
    public string Theme { get; set; }
    public int AmountOfPeople { get; set; }
    public string ImageURL { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public IEnumerable<Batch> Batches { get; set; }
    public IEnumerable<SpeakerEvent> SpeakersEvents { get; set; }
    public IEnumerable<SocialNetwork> SocialNetworks { get; set; }
}
