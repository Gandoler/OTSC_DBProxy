using Domain.DTO.DTO.Friend;
using Domain.DTO.DTO.Pozdr;
using Domain.Interfaces.IServices;
using Domain.Models;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using ProxyAPILeval.DTOExample;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

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

    /// <summary>
    /// Получает список друзей, у которых сегодня день рождения.
    /// </summary>
    [HttpGet("birthdays/today")]
    [SwaggerOperation(Summary = "Получить дни рождения", Description = "Возвращает список друзей, у которых сегодня день рождения.")]
    [SwaggerResponse(200, "Список успешно получен", typeof(IEnumerable<FriendDto>))]
    public async Task<IActionResult> GetTodayBirthdays()
    {
        var friends = await _tgBotService.SelectForTodayBithrdayAsync();
        return Ok(friends);
    }

    /// <summary>
    /// Получает Telegram ID по AppId.
    /// </summary>
    [HttpGet("tgid/{appId:guid}")]
    [SwaggerOperation(Summary = "Получить Telegram ID", Description = "Возвращает Telegram ID по AppId." +
                                                                      "\n\nselect * from tg_comprehensions" +
                                                                      "(550e8400-e29b-41d4-a716-446655440001) - пример")]
    [SwaggerResponse(200, "Telegram ID найден", typeof(TgIdDto))]
    [SwaggerResponse(404, "Telegram ID не найден")]
    public async Task<IActionResult> GetTgId(Guid appId)
    {
        TgIdDto tgId = await _tgBotService.GetTgIdAsync(new AppIdDto { AppId = appId });
        return tgId is not null ? Ok(tgId) : NotFound("Telegram ID not found");
    }

    /// <summary>
    /// Получает поздравление по ID Поздрика.
    /// </summary>
    [HttpPost("getCongrByPID")]
    [SwaggerOperation(Summary = "Получить поздравление", Description = "Возвращает поздравление по ID Поздрика.")]
    [SwaggerRequestExample(typeof(FriendDto), typeof(GetCongrStringExample))]
    [SwaggerResponse(200, "Поздравление найдено", typeof(string))]
    [SwaggerResponse(404, "Поздравление не найдено")]
    public async Task<IActionResult> GetPozdrikById(PozdrikIdDto pozdr)
    {
        string? pozdrString = await _tgBotService.SelectPozdrStringAsync(pozdr);
        return pozdrString != null ? Ok(pozdrString) : NotFound(new { message = "Поздравление не найдено" });
    }

    /// <summary>
    /// Получает Id Поздрика по данным друга.
    /// </summary>
    [HttpPost("getPozdrikId")]
    [SwaggerOperation(Summary = "Получить Id Поздрика", Description = "Возвращает Id Поздрика по данным друга.")]
    [SwaggerRequestExample(typeof(FriendDto), typeof(GetPozdIdInTgExample))]
    [SwaggerResponse(200, "Id Поздрика найден", typeof(PozdrikIdDto))]
    [SwaggerResponse(404, "Поздравление не найдено")]
    public async Task<IActionResult> GetPozdrikId([FromBody] FriendDto friend)
    {
        PozdrikIdDto id = await _tgBotService.GetPozdrikId(friend);
        return id != null ? Ok(id) : NotFound(new { message = "Поздравление не найдено" });
    }
}
