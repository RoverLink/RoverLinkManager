using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace RoverLinkManager.Domain.Enum.Activity;

[EnumAsInt]
public enum ContentType
{
    Undefined = 0,
    Website = 1,
    Image = 2,
    File = 3,
    Video = 4,
    Audio = 5
}
