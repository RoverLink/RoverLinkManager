using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace RoverLinkManager.Domain.Enum.Announcement;

[EnumAsInt]
public enum AnnouncementStatus
{
    Draft = 0,
    Scheduled = 1,
    Sent = 2
}
