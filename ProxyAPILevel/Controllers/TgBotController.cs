// <copyright file="TgBotController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProxyAPILevel;

using Domain.DTO.DTO.Friend;
using Domain.DTO.DTO.Pozdr;
using Domain.Interfaces.IServices;
using Domain.Models;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using ProxyAPILeval.DTOExample;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

// d
[ApiController]
[Route("api/tgbot")]
public class TgBotController : ControllerBase
{
    private readonly ITgBotService tgBotService;

    public TgBotController(ITgBotService tgBotService)
    {
        this.tgBotService = tgBotService;
    }

    /// <summary>
    /// Получает список друзей, у которых сегодня день рождения.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpGet("birthdays/today")]
    [SwaggerOperation(Summary = "Получить дни рождения", Description = "Возвращает список друзей, у которых сегодня день рождения.")]
    [SwaggerResponse(200, "Список успешно получен", typeof(IEnumerable<FriendDto>))]
    public async Task<IActionResult> GetTodayBirthdays()
    {
        var friends = await this.tgBotService.SelectForTodayBithrdayAsync().ConfigureAwait(false);
        return this.Ok(friends);
    }

    /// <summary>
    /// Получает Telegram ID по AppId.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpGet("tgid/{appId:guid}")]
    [SwaggerOperation(Summary = "Получить Telegram ID", Description = "Возвращает Telegram ID по AppId." +
                                                                      "\n\nselect * from tg_comprehensions" +
                                                                      "(550e8400-e29b-41d4-a716-446655440001) - пример")]
    [SwaggerResponse(200, "Telegram ID найден", typeof(TgIdDto))]
    [SwaggerResponse(404, "Telegram ID не найден")]
    public async Task<IActionResult> GetTgId(Guid appId)
    {
        TgIdDto tgId = await this.tgBotService.GetTgIdAsync(new AppIdDto { AppId = appId }).ConfigureAwait(false);
        return tgId is not null ? this.Ok(tgId) : this.NotFound("Telegram ID not found");
    }

    /// <summary>
    /// Получает поздравление по ID Поздрика.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpPost("getCongrByPID")]
    [SwaggerOperation(Summary = "Получить поздравление", Description = "Возвращает поздравление по ID Поздрика.")]
    [SwaggerRequestExample(typeof(FriendDto), typeof(GetCongrStringExample))]
    [SwaggerResponse(200, "Поздравление найдено", typeof(string))]
    [SwaggerResponse(404, "Поздравление не найдено")]
    public async Task<IActionResult> GetPozdrikById(PozdrikIdDto pozdr)
    {
        string? pozdrString = await this.tgBotService.SelectPozdrStringAsync(pozdr).ConfigureAwait(false);
        return pozdrString != null ? this.Ok(pozdrString) : this.NotFound(new { message = "Поздравление не найдено" });
    }

    /// <summary>
    /// Получает Id Поздрика по данным друга.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpPost("getPozdrikId")]
    [SwaggerOperation(Summary = "Получить Id Поздрика", Description = "Возвращает Id Поздрика по данным друга.")]
    [SwaggerRequestExample(typeof(FriendDto), typeof(GetPozdIdInTgExample))]
    [SwaggerResponse(200, "Id Поздрика найден", typeof(PozdrikIdDto))]
    [SwaggerResponse(404, "Поздравление не найдено")]
    public async Task<IActionResult> GetPozdrikId([FromBody] FriendDto friend)
    {
        PozdrikIdDto id = await this.tgBotService.GetPozdrikId(friend).ConfigureAwait(false);
        return id != null ? this.Ok(id) : this.NotFound(new { message = "Поздравление не найдено" });
    }
}
