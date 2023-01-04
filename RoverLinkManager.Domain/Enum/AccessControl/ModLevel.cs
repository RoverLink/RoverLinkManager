using ServiceStack.DataAnnotations;

namespace RoverLinkManager.Domain.Enum.AccessControl;

[EnumAsInt]
public enum ModLevel
{
	NoModeration = 0,
	ModeratePostsFromGroupMembers = 1,
	ModeratePosts = 2
}
