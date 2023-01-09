using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoverLinkManager.Domain.Entities.Identity;
using ServiceStack.DataAnnotations;

namespace RoverLinkManager.Domain.Entities.Announcements;
public class Announcement
{
    public long Id { get; set; }
    public string HashId { get; set; }

    public List<string> Tags { get; set; }

    [Required]
    [StringLength(100)]
    public string EnglishSnippet { get; set; }

    [Required]
    public string EnglishContent { get; set; }

    [StringLength(100)]
    public string SpanishSnippet { get; set; }

    public string SpanishContent { get; set; }

    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;

    public DateTime EditedTime { get; set; } = DateTime.UtcNow;

    public DateTime? ScheduledTime { get; set; }

    public string Status { get; set; }

    public bool IsDeleted { get; set; } = false;

    [ForeignKey(typeof(AppUser))] 
    public int AuthorId { get; set; }
    
    [Reference]
    public AppUser Author { get; set; }

    [ForeignKey(typeof(AppUser))] 
    public int EditorId { get; set; }

    [Reference]
    public AppUser Editor { get; set; }
}
