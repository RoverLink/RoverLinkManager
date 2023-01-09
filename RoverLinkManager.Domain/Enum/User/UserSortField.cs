using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace RoverLinkManager.Domain.Enum.User;

[EnumAsInt]
public enum UserSortField
{
    LastName,
    BirthDay,
    LastLoggedIn
}
