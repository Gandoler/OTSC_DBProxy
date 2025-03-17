using Domain.DTO.DTO.Pozdr;
using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;

namespace ProxyAPILevel;

[ApiController]
[Route("api/mailbot")]
public class MailBotController : ControllerBase
{
    private readonly IMailBotService _mailBotService;

    public MailBotController(IMailBotService mailBotService)
    {
        _mailBotService = mailBotService;
    }

    [HttpGet("birthdays/today")]
    public async Task<IActionResult> GetTodayBirthdays()
    {
        var friends = await _mailBotService.SelectForTodayBithrday();
        return Ok(friends);
    }

    [HttpGet("pozdrik/{pozdrikId:int}")]
    public async Task<IActionResult> GetCongrStr(int pozdrikId)
    {
        var pozdrik = await _mailBotService.SelectPozdStringAsync(new PozdrikIdDto { _pozdrikId = pozdrikId });
        return pozdrik != null ? Ok(pozdrik) : NotFound(new { message = "Pozdrik not found" });
    }

    [HttpGet("email/{appId:guid}")]
    public async Task<IActionResult> GetEmail(Guid appId)
    {
        var email = await _mailBotService.GetEmailAsync(new AppIdDto { AppId = appId });
        return email != null ? Ok(email) : NotFound(new { message = "Email not found" });
    }
}