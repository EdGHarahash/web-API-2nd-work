using PrivateForum.Entities.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateForum.Entities
{
    public class Tag
    {
        public Tag()
        {
            
        }

        public int Id { get; set; }

        [Required]
        [StringLength(35)]
        public string Name { get; set; }

        public virtual ICollection<ApplicationUserTag> ApplicationUserTags { get; set; }
    }
}
