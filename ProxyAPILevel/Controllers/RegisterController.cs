using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;

namespace ProxyAPILeval;

[ApiController]
[Route("api/register")]
public class RegisterController : ControllerBase
{
    private readonly IRegitrService _registerService;

    public RegisterController(IRegitrService registerService)
    {
        _registerService = registerService;
    }

    [HttpPost("create")] 
    public async Task<IActionResult> RegisterUser([FromBody] RegisterDto dto)
    {
        var result = await _registerService.RegisterAsync(dto);
        return result ? Ok(new { message = "User registered successfully" }) : BadRequest(new { message = "Registration failed" });
    }

    [HttpPost("exists")] 
    public async Task<IActionResult> CheckUserExists([FromBody] CheckExistDto dto)
    {
        var exists = await _registerService.ExicstCheckAsync(dto);
        return Ok(new { exists });
    }
}