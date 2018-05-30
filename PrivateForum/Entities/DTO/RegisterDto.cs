using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateForum.Entities.DTO
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }

        public string Tags { get; set; }

        public string InviteToken { get; set; }
        
        public string[] TagsNames { get; set; }

        public void TagsToList() {
            TagsNames = Tags.ToUpper().Split(',');
        }
    }
}
