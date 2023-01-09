using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoverLinkManager.Domain.Entities.Identity;
using RoverLinkManager.Domain.Enum.Announcement;
using ServiceStack.DataAnnotations;

namespace RoverLinkManager.Domain.Entities.Announcements;

public class Announcement
{
    public long Id { get; set; }
    public string HashId { get; set; } = string.Empty;

    public List<string> Tags { get; set; } = new();

    [Required]
    public string Content { get; set; } = string.Empty;

    [PgSqlJsonB]
    public List<Translation> Translations { get; set; } = new();

    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;

    public DateTime EditedTime { get; set; } = DateTime.UtcNow;

    public DateTime? ScheduledTime { get; set; }

    [Default(0)]
    public AnnouncementStatus Status { get; set; } = AnnouncementStatus.Draft;

    public bool IsDeleted { get; set; } = false;

    [ForeignKey(typeof(AppUser))] 
    public int? AuthorId { get; set; }
    
    [Reference]
    public AppUser Author { get; set; }

    public List<int> EditorIds { get; set; }
}
