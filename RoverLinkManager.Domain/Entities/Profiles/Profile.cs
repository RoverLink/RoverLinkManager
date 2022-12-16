using RoverLinkManager.Domain.Entities.Posts;
using RoverLinkManager.Domain.Entities.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverLinkManager.Domain.Entities.Profiles
{
    public class Profile
    {
        public long Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string PictureURL { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int SchoolID { get; set; } // Only if student
        public string FirstName { get; set; } = string.Empty; // Only if student
        public string LastName { get; set; } = string.Empty; // Only if student
        public Profile Child { get; set; } = new Profile(); // Only if parent
        public List<Post> Posts { get; set; } = new List<Post>();
        public List<Profile> FollowingProfiles { get; set; } = new List<Profile>();
        public List<Tag> FollowingTags { get; set; } = new List<Tag>();
        public List<string> Roles { get; set; } = new List<string>(); // Admin, Student, Parent, Staff, or Developer
        public List<Absence> Absences { get; set; } = new List<Absence>(); // Only if student

        //settings
    }
}
