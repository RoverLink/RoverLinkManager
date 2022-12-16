using RoverLinkManager.Domain.Entities.Profiles;
using RoverLinkManager.Domain.Entities.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverLinkManager.Domain.Entities.Posts
{
    public class Announcement
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public Profile Author { get; set; } = new Profile();
        public List<Tag> Tags { get; set; } = new List<Tag>(); // I dont know if this is needed if we use #tag in content
    }
}
