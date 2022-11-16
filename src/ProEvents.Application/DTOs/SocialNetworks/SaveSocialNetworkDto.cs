namespace ProEvents.Application.DTOs.SocialNetworks;
public class SaveSocialNetworkDto
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public string URL { get; set; }
    public long? EventId { get; set; }
    public long? SpeakerId { get; set; }
}
