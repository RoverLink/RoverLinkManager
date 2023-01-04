using ServiceStack.DataAnnotations;

namespace RoverLinkManager.Domain.Enum.AccessControl;

[EnumAsInt]
public enum GroupAccessLevel
{
    GroupOwners = 0, // Default
    GroupManagers = 1,
    GroupMembers = 2,
    OrganizationMembers = 3,
    PublicMembers = 4,
    AnyoneOnWeb = 5
}
