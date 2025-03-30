using HobbyMatch.BL.Models.Auth;
using HobbyMatch.Domain.Requests;

namespace HobbyMatch.BL.Services.Auth.Account;

public interface IAccountService
{
    public Task RegisterBusinessClientAsync(BusinessRegisterRequest registerRequest);
    public Task RegisterUserAsync(UserRegisterRequest registerRequest);
    public Task<AuthResult> LoginUserAsync(LoginRequest loginRequest);
    public Task<AuthResult> RefreshTokenAsync(string? refreshToken);
}