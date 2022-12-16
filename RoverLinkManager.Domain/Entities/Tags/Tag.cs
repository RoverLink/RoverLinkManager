using RoverLinkManager.Domain.Entities.Calendar;
using RoverLinkManager.Domain.Entities.Posts;
using RoverLinkManager.Domain.Entities.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverLinkManager.Domain.Entities.Tags
{
    public class Tag
    {
        public long Id { get; set; }
        public string TagName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string PictureURL { get; set; } = string.Empty;
        public int Followers { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>(); // All posts with #TagName in it
        public bool IsSchool { get; set; }
        public List<Post> OfficialPosts { get; set; } = new List<Post>(); // All posts with #TagName in it from Leaders // Only if not school
        public List<Announcement> Announcements { get; set; } = new List<Announcement>(); // All announcements with #TagName in it // Only if school
        public List<CalendarEvent> Events { get; set; } = new List<CalendarEvent>(); // All events with #TagName in it
        public List<Profile> Leaders { get; set; } = new List<Profile>(); // Only if not school
    }
}
