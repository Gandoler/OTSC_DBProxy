using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using UseCases.Services;

namespace ProxyAPILevel;
[ApiController]
[Route("api/Auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    
    [HttpPost("login")]
    public async Task<IActionResult> CheckUserExists([FromBody] LoginDto loginDto)
    {
        bool exists = await _authService.ExicstCheckAsync(loginDto);
        return Ok(new { exists });
    }
}