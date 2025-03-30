
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HobbyMatch.Domain.Entities
{
    public abstract class Organizer: IdentityUser<int>
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiresAt { get; set; }
    }
}
