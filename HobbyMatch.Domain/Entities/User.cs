using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json.Serialization;

namespace HobbyMatch.Domain.Entities
{
    public class User : Organizer
    {

        [JsonIgnore]
        public ICollection<Event> SignedUpEvents { get; set; }
	}
}
