using HobbyMatch.BL.DTOs.Auth;
using HobbyMatch.BL.Services.Auth.Account;
using HobbyMatch.Domain.Requests;
using HobbyMatchAPI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HobbyMatch.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAccountService _accountService;
    public AuthController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _accountService.LoginUserAsync(request);
        return Ok(AuthResultDto.FromAuthResult(result));
    }

    [HttpPost("register/business")]
    public async Task<IActionResult> Register([FromBody] BusinessRegisterRequest request)
    {
        await _accountService.RegisterBusinessClientAsync(request);
        return Ok();
    }
    
    [HttpPost("register/user")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
    {
        await _accountService.RegisterUserAsync(request);
        return Ok();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.GetRefreshToken();
        var result = await _accountService.RefreshTokenAsync(refreshToken);

        return Ok(AuthResultDto.FromAuthResult(result));
    }
}