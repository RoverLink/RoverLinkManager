using ServiceStack.DataAnnotations;

namespace RoverLinkManager.Domain.Enum.AccessControl;

// Who can see your post
[EnumAsInt]
public enum PostAudience
{
	OnlyMe = 0,   // Shows only to you (not posted to any other feeds), not available for group posts
	Friends = 1, // Shows only to your friends, not available for group posts
	Group = 2,   // Shows up only to group members (this is only available when posting to groups)
    Organization = 3, // Shows up to anyone belonging to the organization
    Public = 4  // Shows up in news feed, profile, in search results (reposted to friends feed as well)
}
