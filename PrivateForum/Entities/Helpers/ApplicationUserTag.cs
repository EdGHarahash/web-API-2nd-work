using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateForum.Entities.Helpers
{
    public class ApplicationUserTag
    {
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public int TagId { get; set; }
        
        public Tag Tag { get; set; }
    }
}
