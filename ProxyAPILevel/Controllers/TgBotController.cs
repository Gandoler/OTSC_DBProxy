using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;

namespace ProxyAPILevel;

[ApiController]
[Route("api/tgbot")]
public class TgBotController : ControllerBase
{
    private readonly ITgBotService _tgBotService;

    public TgBotController(ITgBotService tgBotService)
    {
        _tgBotService = tgBotService;
    }

    [HttpGet("birthdays/today")]
    public async Task<IActionResult> GetTodayBirthdays()
    {
        var friends = await _tgBotService.SelectForTodayBithrdayAsync();
        return Ok(friends);
    }

    [HttpGet("tgid/{appId:guid}")]
    public async Task<IActionResult> GetTgId(Guid appId)
    {
        var tgId = await _tgBotService.GetTgIdAsync(new AppIdDto { AppId = appId });
        return tgId != null ? Ok(tgId) : NotFound(new { message = "Telegram ID not found" });
    }
}