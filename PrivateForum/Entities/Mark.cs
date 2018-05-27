using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateForum.Entities
{
    public class Mark
    {
        public int Id { get; set; }
        [Range(1, 5)]
        public int Value { get; set; }
        public bool Reject { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
