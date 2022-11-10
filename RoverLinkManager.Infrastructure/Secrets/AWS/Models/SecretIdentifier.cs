using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RoverLinkManager.Infrastructure.Secrets.AWS.Models;

public class SecretIdentifier
{
    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("Region")]
    public string Region { get; set; }
}