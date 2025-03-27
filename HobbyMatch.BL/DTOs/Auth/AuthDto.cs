using HobbyMatch.BL.Models.Auth;

namespace HobbyMatch.BL.DTOs.Auth;

public record AuthResultDto(
    string JwtToken,
    DateTime JwtTokenExpirationDate,
    string RefreshToken,
    DateTime RefreshTokenExpirationDate
)
{
    public static AuthResultDto FromAuthResult(AuthResult authResult)
    {
        return new(
            authResult.JwtToken,
            authResult.JwtTokenExpirationDate,
            authResult.RefreshToken,
             authResult.RefreshTokenExpirationDate
        );
    }
}
