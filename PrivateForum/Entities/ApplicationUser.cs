
using Microsoft.AspNetCore.Identity;
using PrivateForum.Entities.Helpers;
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
        public int InviteCount { get; set; }
        public virtual ApplicationUser Parent { get; set; }
        public virtual ICollection<ApplicationUser> Invited { get; set; }
        public virtual ICollection<ApplicationUserTag> ApplicationUserTags { get; set; }
        public virtual ICollection<Mark> Marks { get; set; }
        // public virtual ICollection<Invite> Invites{ get; set; }
        public ApplicationUser()
        {
           
        }
    }
}
