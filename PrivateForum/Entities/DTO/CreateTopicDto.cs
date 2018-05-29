using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateForum.Entities.DTO
{
    public class CreateTopicDto
    {
        [Required]
        [StringLength(35, MinimumLength = 5)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }
        public virtual string Tag { get; set; }

        public Topic GetTopic() => new Topic
        {
            Tag = new Tag { Name = Tag.ToUpper() },
            Description = Description,
            Name = Name
        };
    }
}
