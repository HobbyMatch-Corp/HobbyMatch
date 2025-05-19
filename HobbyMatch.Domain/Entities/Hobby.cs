using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HobbyMatch.Domain.Entities
{
    public class Hobby
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Hobby name must be 5-50 characters"), MinLength(1)]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<User> Users { get; set; } = new List<User>();

        [JsonIgnore]
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
