using Domain.DTO.DTO.MailComp;
using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;

namespace ProxyAPILevel;

[ApiController]
[Route("api/register")]
public class RegisterController : ControllerBase
{
    private readonly IRegistrService _registerService;

    public RegisterController(IRegistrService registerService)
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
        if (exists) return Ok(new { message = "User already exists" });
        return BadRequest(new { message = "User already exists" });
      
    }
    [HttpGet("UserId")] 
    public async Task<IActionResult> GetUserIdByLogin([FromBody] CheckExistDto dto)
    {
        AppIdDto? appIdDto = await _registerService.GetAppId(dto);
        if (appIdDto is null) return BadRequest(new { message = "User does not exist" });
        return Ok(appIdDto);
      
    }
    
    [HttpPut("Mail")] 
    public async Task<IActionResult> GetUserIdByLogin([FromBody] ADDMailDto dto)
    {
        bool add = await _registerService.AddMail(dto);
        if (add) return Ok();
        return BadRequest(new { message = "mail already exist exist" });
      
    }
}