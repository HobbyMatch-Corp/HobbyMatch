using HobbyMatch.BL.DTOs.Auth;
using HobbyMatch.BL.Models.Auth;
using HobbyMatch.Domain.Requests;

namespace HobbyMatch.BL.Services.Auth.Account;

public interface IAccountService
{
    public Task RegisterBusinessClientAsync(BusinessRegisterDto registerRequest);
    public Task RegisterUserAsync(UserRegisterDto registerRequest);
    public Task<AuthResult> LoginUserAsync(LoginRequestDto loginRequest);
    public Task<AuthResult> RefreshTokenAsync(string? refreshToken);
}