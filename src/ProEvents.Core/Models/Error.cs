using System.Text.Json.Serialization;

namespace ProEvents.Core.Models;
public class Error
{
    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }
}
