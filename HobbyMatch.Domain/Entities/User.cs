using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HobbyMatch.Domain.Entities
{
    public class User : Organizer
    {

        [JsonIgnore]
        public ICollection<Event> SignedUpEvents { get; set; } = new List<Event>();

        [JsonIgnore]
        public ICollection<User> Friends { get; } = new List<User>();

        [JsonIgnore]
        public ICollection<Hobby> Hobbies { get; set; } = new List<Hobby>();
    }
}
