using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RoverLinkManager.Domain.Entities.Settings;
public class ApplicationSettings
{
    [JsonPropertyName("GeneratorId")]
    public int GeneratorId { get; set; }

    [JsonPropertyName("firebaseid")]
    public string FirebaseId { get; set; } = string.Empty;

    [JsonPropertyName("hashidsalt")]
    public string HashIdSalt { get; set; } = string.Empty;

    [JsonPropertyName("email")] 
    public EmailSettings Email { get; set; } = new();

    [JsonPropertyName("database")]
    public DatabaseSettings Database { get; set; } = new();

    [JsonPropertyName("stream")]
    public StreamSettings Stream { get; set; } = new();

    [JsonPropertyName("timezone")]
    public string TimeZone { get; set; } = TimeZoneInfo.Local.Id;
}
