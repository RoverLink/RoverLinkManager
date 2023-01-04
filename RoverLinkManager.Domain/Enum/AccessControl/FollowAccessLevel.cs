using ServiceStack.DataAnnotations;

namespace RoverLinkManager.Domain.Enum.AccessControl;

[EnumAsInt]
public enum FollowAccessLevel
{
    InvitedUsersOnly = 0,
    AnyoneInAllowedGroups = 1,
    OrganizationMembers = 2,
    AnyoneOnWeb = 3  // Default
}
