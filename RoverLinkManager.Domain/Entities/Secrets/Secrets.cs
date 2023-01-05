using System.Text.Json.Serialization;
using RoverLinkManager.Domain.Entities.Settings;

namespace RoverLinkManager.Domain.Entities.Secrets;

public class Secrets
{
    [JsonPropertyName("settings")]
    public ApplicationSettings Settings { get; set; }

    [JsonPropertyName("database")]
    public DbCredentialsSecret Database { get; set; }
}
