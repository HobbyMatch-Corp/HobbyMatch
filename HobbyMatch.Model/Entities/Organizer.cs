using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.Model.Entities
{
    public class Organizer
    {

        public int ID { get; set; }

        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "Username should be between 2 and 50 characters.")]
        public string Username { get; set; }

        public string Password { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }
    }
}
