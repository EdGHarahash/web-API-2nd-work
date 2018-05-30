using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateForum.Entities
{
    public class Invite
    {
        public Invite(ApplicationUser user)
        {
            User = user;
            InviteToken = new string(user.Email.ToCharArray().Reverse().ToArray()) + DateTime.Now;
        }

        public Invite() { }

        public int Id { get; set; }
        public string InviteToken { get; set; }
        public ApplicationUser User { get; set; }
    }
}
