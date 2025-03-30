using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Services.Auth;

public interface ITokenGenerator
{
    public (string token, DateTime expirationDate) GenerateToken(Organizer user);
    public string GenerateRefreshToken();
}