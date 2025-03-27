
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HobbyMatch.Model.Entities
{
    public abstract class Organizer: IdentityUser<int>
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiresAt { get; set; }
    }
}
