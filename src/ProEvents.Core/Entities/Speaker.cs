using ProEvents.Core.Identity.Model;

namespace ProEvents.Core.Entities;
public class Speaker : EntityBase
{
    public string Resume { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }

    // EF Relational
    public IEnumerable<SocialNetwork> SocialNetworks { get; set; }
    public IEnumerable<SpeakerEvent> SpeakersEvents { get; set; }

}
