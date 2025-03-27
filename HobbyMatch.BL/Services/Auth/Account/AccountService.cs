using System.Net;
using HobbyMatch.BL.Models.Auth;
using HobbyMatch.Database.Repositories.User;
using HobbyMatch.Model.Entities;
using HobbyMatch.Model.Exceptions;
using HobbyMatch.Model.Exceptions.AuthExceptions;
using HobbyMatch.Model.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HobbyMatch.BL.Services.Auth.Account;

public class AccountService: IAccountService
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly UserManager<Organizer> _userManager;
    private readonly IUserRepository _userRepository;

    public AccountService(ITokenGenerator tokenGenerator, UserManager<Organizer> userManager,IUserRepository userRepository)
    {
        _tokenGenerator = tokenGenerator;
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task RegisterBusinessClientAsync(BusinessRegisterRequest registerRequest)
    {
        var userExists = await _userManager.Users.AnyAsync(u => u.Email == registerRequest.Email);
        if (userExists)
        {
            throw new UserAlreadyExistsException(registerRequest.Email);
        }

        var newBusinessClient = new BusinessClient
        {
            Email = registerRequest.Email,
            TaxID = registerRequest.TaxId,
        };
        var result = await _userManager.CreateAsync(newBusinessClient,registerRequest.Password);

        if (!result.Succeeded)
        {
            throw new RegistrationFailedException(result.Errors.Select(err => err.Description));
        }
    }
    
    public async Task RegisterUserAsync(UserRegisterRequest registerRequest)
    {
        var userExists = await _userManager.FindByEmailAsync(registerRequest.Email) != null;
        if (userExists)
        {
            throw new UserAlreadyExistsException(registerRequest.Email);
        }

        var newUser = new User
        {
            Email = registerRequest.Email,
        };
        var result = await _userManager.CreateAsync(newUser,registerRequest.Password);

        if (!result.Succeeded)
        {
            throw new RegistrationFailedException(result.Errors.Select(err => err.Description));
        }
    }

    public async Task<AuthResult> LoginUserAsync(LoginRequest loginRequest)
    {
        var user = await _userManager.FindByEmailAsync(loginRequest.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
        {
            throw new LoginFailedException(loginRequest.Email);
        }

        var (jwtToken, expiresAt) = _tokenGenerator.GenerateToken(user);
        var refreshToken = _tokenGenerator.GenerateRefreshToken();

        var refreshTokenExpiration = DateTime.UtcNow.AddDays(7);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiresAt = refreshTokenExpiration;

        await _userManager.UpdateAsync(user);
        
        return new AuthResult
        {
            JwtToken =  jwtToken,
            JwtTokenExpirationDate = expiresAt,
            RefreshToken = refreshToken,
            RefreshTokenExpirationDate = refreshTokenExpiration
        };
    }

    public async Task<AuthResult> RefreshTokenAsync(string? refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
        {
            throw new RefreshTokenException("Refresh token is missing.");
        }

        var user = await _userRepository.GetUserByRefreshTokenAsync(refreshToken);

        if (user == null)
        {
            throw new RefreshTokenException("Unable to retrieve user for refresh token");
        }

        if (user.RefreshTokenExpiresAt < DateTime.UtcNow)
        {
            throw new RefreshTokenException("Refresh token is expired.");
        }
        
        var (jwtToken, expiresAt) = _tokenGenerator.GenerateToken(user);
        var newRefreshToken = _tokenGenerator.GenerateRefreshToken();

        var refreshTokenExpiration = DateTime.UtcNow.AddDays(7);

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiresAt = refreshTokenExpiration;

        await _userManager.UpdateAsync(user);
        
        return new AuthResult
        {
            JwtToken =  jwtToken,
            JwtTokenExpirationDate = expiresAt,
            RefreshToken = refreshToken,
            RefreshTokenExpirationDate = refreshTokenExpiration
        };
    }
}