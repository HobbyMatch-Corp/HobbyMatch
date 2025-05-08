using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json.Serialization;

namespace HobbyMatch.Domain.Entities
{
    public class User : Organizer
    {

        [JsonIgnore]
        public ICollection<Event> SignedUpEvents { get; set; }
        [JsonIgnore]
        public ICollection<User> Friends { get; set; } = new List<User>();
        [JsonIgnore]
        public ICollection<User>? FriendOf { get; set; } = new List<User>();
    }
}
