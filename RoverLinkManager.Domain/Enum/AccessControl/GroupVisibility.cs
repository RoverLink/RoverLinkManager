using ServiceStack.DataAnnotations;

namespace RoverLinkManager.Domain.Enum.AccessControl;

[EnumAsInt]
public enum GroupVisibility
{
    Unpublished = 0,
    Published = 1
}
