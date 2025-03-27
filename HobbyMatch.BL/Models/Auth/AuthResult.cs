namespace HobbyMatch.BL.Models.Auth;

public class AuthResult
{
    public required string JwtToken { get; set; }
    public required DateTime JwtTokenExpirationDate { get; set; }
    public required string RefreshToken { get; set; }
    public required DateTime RefreshTokenExpirationDate { get; set; }
}