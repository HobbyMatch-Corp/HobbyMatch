using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HobbyMatch.BL.Configuration;
using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain.Enums;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HobbyMatch.BL.Services.Auth.Tokens;

public class JwtTokenGenerator : ITokenGenerator
{
    private readonly JwtOptions _options;
    public JwtTokenGenerator(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }
    public (string token, DateTime expirationDate) GenerateToken(Organizer user)
    {
        var signingKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_options.Secret));

        var credentials = new SigningCredentials(
            signingKey, SecurityAlgorithms.HmacSha512);

        var userType = user is User ? UserType.User : UserType.BussinessClient;
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            new Claim("userType",userType.ToString()),
        };

        var expiration = DateTime.UtcNow.AddMinutes(_options.ExpirationMinutes);
        var token = new JwtSecurityToken(
            claims: claims,
            issuer: _options.Issuer, audience: _options.Audience, expires: expiration, signingCredentials: credentials);

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
        return (jwtToken, expiration);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

}