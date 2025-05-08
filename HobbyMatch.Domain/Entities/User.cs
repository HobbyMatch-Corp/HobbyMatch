using System.Text.Json.Serialization;

namespace HobbyMatch.Domain.Entities
{
    public class User : Organizer
    {

        [JsonIgnore]
        public ICollection<Event> SignedUpEvents { get; set; }
        [JsonIgnore]
        public ICollection<User> Friends { get; } = new List<User>();
        [JsonIgnore]
        public ICollection<User>? FriendOf { get; } = new List<User>();
    }
}
