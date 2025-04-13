using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace HobbyMatch.Domain.Entities
{
    public abstract class Organizer : IdentityUser<int>
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiresAt { get; set; }

        [JsonIgnore]
        public ICollection<Event> OrganizedEvents { get; set; }
    }
}
