using HobbyMatch.BL.DTOs.Auth;
using HobbyMatch.BL.Services.Auth.Account;
using HobbyMatch.Domain.Requests;
using HobbyMatchAPI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HobbyMatch.API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAccountService _accountService;
    public AuthController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var result = await _accountService.LoginUserAsync(request);
        return Ok(result.ToDto());
    }

    [HttpPost("register/business")]
    public async Task<IActionResult> Register([FromBody] BusinessRegisterDto request)
    {
        await _accountService.RegisterBusinessClientAsync(request);
        return Ok();
    }

    [HttpPost("register/user")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto request)
    {
        await _accountService.RegisterUserAsync(request);
        return Ok();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.GetRefreshToken();
        var result = await _accountService.RefreshTokenAsync(refreshToken);

        return Ok(result.ToDto());
    }
}