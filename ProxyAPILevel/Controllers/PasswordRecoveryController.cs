using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;

namespace ProxyAPILevel;

[ApiController]
[Route("api/passwordrecovery")]
public class PasswordRecoveryController : ControllerBase
{
    private readonly IPasswordRecoveryService _passwordRecoveryService;

    public PasswordRecoveryController(IPasswordRecoveryService passwordRecoveryService)
    {
        _passwordRecoveryService = passwordRecoveryService;
    }

    [HttpGet("getid/{email}")]
    public async Task<IActionResult> GetIdByEmail(string email)
    {
        var result = await _passwordRecoveryService.GetIdByMailAsync(email);
        return Ok(result);
    }

    [HttpGet("check-mail/{email}")]
    public async Task<IActionResult> CheckMail(string mail)
    {
        var result = await _passwordRecoveryService.ExistByMailAsync(mail);
        return Ok(result);
    }

    [HttpGet("exists/{login}")] 
    public async Task<IActionResult> CheckUserExists( string login)
    {
        var exists = await _passwordRecoveryService.ExicstCheckByLoginAsync(login);
        return Ok(new { exists });
    }

    [HttpPut("update")] 
    public async Task<IActionResult> UpdateUser([FromBody] LoginDto loginDto)
    {
        var result = await _passwordRecoveryService.UpdateAsync(loginDto);
        return result ? Ok(new { message = "Password updated successfully" }) : BadRequest(new { message = "Update failed" });
    }
}