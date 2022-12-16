using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverLinkManager.Domain.Entities.Profiles
{
    public class Absence
    {
        public long Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Reason { get; set; } = string.Empty;
        public Profile Reporter { get; set; } = new Profile(); // Only parents can report absences
        public Profile Student { get; set; } = new Profile(); // Only students can be reported
        
    }
}
