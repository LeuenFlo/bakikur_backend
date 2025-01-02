using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BakikurBackend.Models;

namespace BakikurBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly AdminSettings _adminSettings;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        IConfiguration configuration,
        IOptions<AdminSettings> adminSettings,
        ILogger<AuthController> logger)
    {
        _configuration = configuration;
        _adminSettings = adminSettings.Value;
        _logger = logger;
    }

    [HttpPost("login")]
    public ActionResult<string> Login(AdminLoginDto loginDto)
    {
        if (loginDto.Username != _adminSettings.Username || 
            loginDto.Password != _adminSettings.Password)
        {
            _logger.LogWarning("Failed login attempt for username: {Username}", loginDto.Username);
            return Unauthorized();
        }

        var token = GenerateJwtToken();
        return Ok(new { token });
    }

    private string GenerateJwtToken()
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? 
                throw new InvalidOperationException("JWT Key not found")));
        
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, _adminSettings.Username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var tokenLifetimeMinutes = _configuration.GetValue<int>("Jwt:TokenLifetimeMinutes");
        if (tokenLifetimeMinutes <= 0)
        {
            tokenLifetimeMinutes = 180; // Fallback auf 3 Stunden wenn nicht konfiguriert
            _logger.LogWarning("TokenLifetimeMinutes not configured or invalid, using default value of 180 minutes");
        }

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(tokenLifetimeMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
} 