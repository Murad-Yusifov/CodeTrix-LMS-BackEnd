using System.Security.Claims;
using BackEndCodeTrix.Src.Auth.AuthDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndCodeTrix.Src.Auth;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(
        IAuthService service)
    {
        _service = service;
    }

    // =========================================
    // PUBLIC
    // =========================================

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(
        LoginDto dto)
    {
        var result =
            await _service.LoginAsync(dto);

        return Ok(result);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(
        RegisterDto dto)
    {
        var result =
            await _service.RegisterAsync(dto);

        return Ok(result);
    }

    // =========================================
    // AUTHENTICATED
    // =========================================

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> Me()
    {
        var userIdClaim =
            User.FindFirstValue(
                ClaimTypes.NameIdentifier);

        if (!int.TryParse(
                userIdClaim,
                out var userId))
        {
            return Unauthorized();
        }

        var result =
            await _service.GetMeAsync(userId);

        return Ok(result);
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh(
        RefreshTokenDto dto)
    {
        var result =
            await _service.RefreshAsync(dto);

        return Ok(result);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var userIdClaim =
            User.FindFirstValue(
                ClaimTypes.NameIdentifier);

        if (!int.TryParse(
                userIdClaim,
                out var userId))
        {
            return Unauthorized();
        }

        await _service.LogoutAsync(userId);

        return NoContent();
    }
}