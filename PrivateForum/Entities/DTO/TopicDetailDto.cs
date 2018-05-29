using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateForum.Entities.DTO
{
    public class TopicDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AverangeMark { get; set; }
        public int MarkCount { get; set; }
        public virtual string Tag { get; set; }
        public virtual string User { get; set; }

        public TopicDetailDto(Topic topic) {
            Id = topic.Id;
            Name = topic.Name;
            Description = topic.Description;
            AverangeMark = topic.AverangeMark;
            MarkCount = topic.MarkCount;
            Tag = topic.Tag?.Name;
            User = topic.User.UserName;
        }
    }
}
