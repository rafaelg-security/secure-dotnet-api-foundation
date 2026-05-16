using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using SecureDotnetApiFoundation.Api.Data;
using SecureDotnetApiFoundation.Api.Dtos;
using SecureDotnetApiFoundation.Api.Services;

namespace SecureDotnetApiFoundation.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly TokenService _tokenService;
    private readonly AuditService _audit;

    public AuthController(
        AppDbContext db,
        TokenService tokenService,
        AuditService audit)
    {
        _db = db;
        _tokenService = tokenService;
        _audit = audit;
    }

    [HttpPost("login")]
    [EnableRateLimiting("AuthPolicy")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            await _audit.LogAsync(
                "LoginAttempt",
                "Failed",
                "Invalid login request payload"
            );

            return BadRequest(ModelState);
        }

        var user = await _db.Users
            .SingleOrDefaultAsync(x => x.Username == request.Username);

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            await _audit.LogAsync(
                "LoginAttempt",
                "Failed",
                $"Failed login attempt for username: {request.Username}"
            );

            return Unauthorized();
        }

        var token = _tokenService.CreateToken(user);

        await _audit.LogAsync(
            "LoginAttempt",
            "Success",
            $"Successful login for username: {user.Username}"
        );

        return Ok(new LoginResponse
        {
            AccessToken = token.AccessToken,
            ExpiresAtUtc = token.ExpiresAtUtc
        });
    }
}