using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using RoverLinkManager.Domain.Entities.Settings;

namespace RoverLinkManager.Infrastructure.Secrets.AWS.Models;
public class Secrets
{
    [JsonPropertyName("settings")]
    public ApplicationSettings Settings { get; set; }

    [JsonPropertyName("database")]
    public DbCredentialsSecret Database { get; set; }
}
