namespace ProEvents.Core.Entities;
public class SocialNetwork : EntityBase
{
    public string Name { get; set; }
    public string URL { get; set; }

    // EF Relational
    public long? EventId { get; set; }
    public Event Event { get; set; }
    public long? SpeakerId { get; set; }
    public Speaker Speaker { get; set; }
}
