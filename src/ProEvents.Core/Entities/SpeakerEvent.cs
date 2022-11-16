namespace ProEvents.Core.Entities;
public class SpeakerEvent
{
    public long EventId { get; set; }
    public Event Event { get; set; }
    public long SpeakerId { get; set; }
    public Speaker Speaker { get; set; }
}
