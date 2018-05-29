using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateForum.Entities.DTO
{
    public class AllTopicDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AverangeMark { get; set; }
        public virtual string Tag { get; set; }
        public virtual string User { get; set; }

        public AllTopicDto(Topic topic)
        {
            Id = topic.Id;
            Name = topic.Name;            
            AverangeMark = topic.AverangeMark;
            Tag = topic.Tag?.Name;
            User = topic.User.UserName;
        }
        static public List<AllTopicDto> MakeList(List<Topic> topics) {
            List<AllTopicDto> dto = new List<AllTopicDto>();
            foreach(Topic t in topics){
                dto.Add(new AllTopicDto(t));
            }
            return dto;
        }
    }
}
