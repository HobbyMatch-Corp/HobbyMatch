namespace HobbyMatch.BL.DTOs.Auth;

public record AuthResultDto(
    string JwtToken,
    DateTime JwtTokenExpirationDate,
    string RefreshToken,
    DateTime RefreshTokenExpirationDate
);
