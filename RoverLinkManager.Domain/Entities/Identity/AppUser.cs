using RoverLinkManager.Domain.Entities;
using ServiceStack.Auth;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace RoverLinkManager.Domain.Entities.Identity;

public class AppUser : UserAuth
{
    [Index]
    public string FirebaseUid { get; set; } = string.Empty;
	public bool IsOrganizationMember { get; set; } = false;
    [ForeignKey(typeof(Building))]
    public long? BuildingId { get; set; }
    [Reference]
    public Building Building { get; set; } = new();
	public string Occupation { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
    [Default(0)]
    public int FollowersCount { get; set; }
    [Default(0)]
    public int FriendsCount { get; set; }
    public string HomeTown { get; set; } = string.Empty;
    public bool Notifications { get; set; } 
    public string? ProfilePhotoUrl { get; set; }
    public string? ProfileCoverUrl { get; set; }
    public string? LastLoginIp { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public bool Verified { get; set; }
}