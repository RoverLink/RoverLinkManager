using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RoverLinkManager.Domain.Entities.Settings;

public class EmailSettings
{
    [Required]
    [DisplayName("Default Sender Address")]
    [DataType(DataType.EmailAddress)]
    [JsonPropertyName("defaultSenderAddress")]
    public string DefaultSenderAddress { get; set; } = string.Empty;
    [Required]
    [DisplayName("Default Sender Name")]
    [JsonPropertyName("defaultSenderName")]
    public string DefaultSenderName { get; set; } = string.Empty;
    [JsonPropertyName("server")]
    public string Server { get; set; } = string.Empty;
    [JsonPropertyName("port")]
    public int Port { get; set; } = 25;
    [JsonPropertyName("user")]
    public string User { get; set; } = string.Empty;
    [DataType(DataType.Password)]
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
    [DisplayName("Use SSL?")]
    [JsonPropertyName("useSsl")]
    public bool UseSsl { get; set; } = false;
    [DisplayName("Require Authentication?")]
    [JsonPropertyName("requiresAuthentication")]
    public bool RequiresAuthentication { get; set; } = false;
    [DisplayName("Preferred Encoding")]
    [JsonPropertyName("preferredEncoding")]
    public string PreferredEncoding { get; set; } = string.Empty;
    [DisplayName("Use Pickup Directory?")]
    [JsonPropertyName("usePickupDirectory")]
    public bool UsePickupDirectory { get; set; } = false;
    [DisplayName("Mail Pickup Directory Server Path")]
    [JsonPropertyName("mailPickupDirectory")]
    public string MailPickupDirectory { get; set; } = string.Empty;
}
