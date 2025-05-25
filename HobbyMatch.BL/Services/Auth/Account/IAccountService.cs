using HobbyMatch.BL.DTOs.Auth;

namespace HobbyMatch.BL.Services.Auth.Account;

public interface IAccountService
{
    public Task RegisterBusinessClientAsync(BusinessRegisterDto registerRequest);
    public Task RegisterUserAsync(UserRegisterDto registerRequest);
    public Task<AuthResultDto> LoginUserAsync(LoginRequestDto loginRequest);
    public Task<AuthResultDto> RefreshTokenAsync(string? refreshToken);
}