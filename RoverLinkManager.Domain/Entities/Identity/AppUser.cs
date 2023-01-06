using RoverLinkManager.Domain.Entities;
using ServiceStack.Auth;
using ServiceStack.DataAnnotations;

namespace RoverLinkManager.Domain.Entities.Identity;

public class AppUser : UserAuth
{
	public bool IsOrganizationMember { get; set; } = false;
    public long? BuildingId { get; set; }
    [Reference]
    public Building Building { get; set; } = new();
	public string Position { get; set; } = string.Empty;
	public string Bio { get; set; } = string.Empty;
    public int Followers { get; set; }
    public string? ProfileUrl { get; set; }
    public string? LastLoginIp { get; set; }
    public DateTime? LastLoginDate { get; set; }
}