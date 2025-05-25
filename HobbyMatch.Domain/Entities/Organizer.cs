using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace HobbyMatch.Domain.Entities;

public abstract class Organizer : IdentityUser<int>
{
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiresAt { get; set; }

    [JsonIgnore]
    public ICollection<Event> OrganizedEvents { get; set; } = [];

    [JsonIgnore]
    public ICollection<Comment> Comments { get; set; } = [];
}
