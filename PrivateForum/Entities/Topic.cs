using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateForum.Entities
{
    public class Topic
    {
        public int Id { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 5)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Description { get; set; }

        public bool Published { get; set; }
        public int AverangeMark { get; set; }
        public int MarkCount { get; set; }
        public virtual ICollection<Mark> Marks { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
