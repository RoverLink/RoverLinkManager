using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RoverLinkManager.Domain.Entities.Settings;
public class ApplicationSettings
{
    [JsonPropertyName("email")]
    public EmailSettings Email { get; set; }
}
