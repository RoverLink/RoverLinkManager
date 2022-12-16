using RoverLinkManager.Domain.Entities.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverLinkManager.Domain.Entities.Calendar
{
    public class CalendarEvent
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Start { get; set; } = DateTime.UtcNow;
        public DateTime End { get; set; } = DateTime.UtcNow;
        public int RepeatEvery { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>(); // I dont know if this is needed if we use #tag in content
    }
}
