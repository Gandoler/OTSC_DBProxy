using Domain.DTO.DTO.Pozdr;
using Domain.Interfaces.IServices;
using Domain.Models;
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
        TgIdDto tgId = await _tgBotService.GetTgIdAsync(new AppIdDto { AppId = appId });
        return tgId is not null ? Ok(tgId) : NotFound("Telegram ID not found");
    }
    [HttpGet("getCongrByPID/{pozdr:int}")]
    public async Task<IActionResult> GetTgId(PozdrikIdDto pozdr)
    {
        string? pozdrString = await _tgBotService.SelectPozdrStringAsync(pozdr);
        return pozdrString != null ? Ok(pozdrString) : NotFound(new { message = "Поздравление не найдено" });
    }
    
    
}