using System.Text.Json.Serialization;

namespace RoverLinkManager.Domain.Entities.Secrets;

public class SecretIdentifier
{
    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("Region")]
    public string Region { get; set; }
}