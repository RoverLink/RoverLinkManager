using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RoverLinkManager.Domain.Entities.Settings;

public class StreamSettings
{
    [JsonPropertyName("stream_key")]
    public string? StreamKey { get; set; }

    [JsonPropertyName("stream_appid")]
    public string? StreamAppId { get; set; }

    [JsonPropertyName("stream_secret")]
    public string? StreamSecret { get; set; }
}
