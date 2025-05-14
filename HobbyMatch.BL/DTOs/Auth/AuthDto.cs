using HobbyMatch.BL.Models.Auth;

namespace HobbyMatch.BL.DTOs.Auth;

public record AuthResultDto(
    string JwtToken,
    DateTime JwtTokenExpirationDate,
    string RefreshToken,
    DateTime RefreshTokenExpirationDate
);

public static partial class AuthResultExtensions
{
    public static AuthResultDto ToDto(this AuthResult authResult) =>
        new(
            authResult.JwtToken,
            authResult.JwtTokenExpirationDate,
            authResult.RefreshToken,
             authResult.RefreshTokenExpirationDate
        );
}
