using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace RoverLinkManager.Domain.Entities.Link;

public class Link
{
    [PrimaryKey]
    public long Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string ShortId { get; set; } = string.Empty;
    public long VisitCount { get; set; } = 0;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
