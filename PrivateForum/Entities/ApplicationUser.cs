
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateForum.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public int Respect { get; set; }
        // public List<Comment> Comments { get; set; }
        // public List<Topic> Topics { get; set; }
        public virtual ApplicationUser Parent { get; set; }
        public virtual ICollection<ApplicationUser> Invited { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Mark> Marks { get; set; }
        public ApplicationUser()
        {
            Tags = new HashSet<Tag>();
        }
    }
}
