// <copyright file="MailBotController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProxyAPILevel;

using Domain.DTO.DTO.Friend;
using Domain.DTO.DTO.Pozdr;
using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using ProxyAPILeval.DTOExample;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

[ApiController]
[Route("api/mailbot")]
public class MailBotController : ControllerBase
{
    private readonly IMailBotService mailBotService;

    public MailBotController(IMailBotService mailBotService)
    {
        this.mailBotService = mailBotService;
    }

    /// <summary>
    /// Получает список друзей с днями рождения сегодня.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpGet("birthdays/today")]
    [SwaggerOperation(Summary = "Получить сегодняшние дни рождения", Description = "Возвращает список друзей, у которых сегодня день рождения.")]
    [SwaggerResponse(200, "Список успешно получен")]
    public async Task<IActionResult> GetTodayBirthdays()
    {
        var friends = await this.mailBotService.SelectForTodayBithrday().ConfigureAwait(false);
        return this.Ok(friends);
    }

    /// <summary>
    /// Получает строку поздравления по ID.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpGet("GetCongrString/{pozdrikId:int}")]
    [SwaggerOperation(Summary = "Получить поздравление", Description = "Возвращает поздравительную строку по указанному ID Поздрика.")]
    [SwaggerResponse(200, "Поздравление успешно найдено")]
    [SwaggerResponse(404, "Поздравление не найдено")]
    public async Task<IActionResult> GetCongrStr(int pozdrikId)
    {
        var pozdrik = await this.mailBotService.SelectPozdStringAsync(new PozdrikIdDto { PozdrikId = pozdrikId }).ConfigureAwait(false);
        return pozdrik != null ? this.Ok(pozdrik) : this.NotFound(new { message = "Pozdrik not found" });
    }

    /// <summary>
    /// Получает email по AppId.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpGet("email/{appId:guid}")]
    [SwaggerOperation(Summary = "Получить email", Description = "Возвращает email по указанному AppId." +
                                                                "\n\n(550e8400-e29b-41d4-a716-446655440000) - пример")]
    [SwaggerResponse(200, "Email успешно найден")]
    [SwaggerResponse(404, "Email не найден")]
    public async Task<IActionResult> GetEmail(Guid appId)
    {
        var email = await this.mailBotService.GetEmailAsync(new AppIdDto { AppId = appId }).ConfigureAwait(false);
        return email != null ? this.Ok(email) : this.NotFound(new { message = "Email not found" });
    }

    // tested

    /// <summary>
    /// Получает Id Поздрика по данным друга.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpPost("getPozdrikId")]
    [SwaggerOperation(Summary = "Получить Id Поздрика", Description = "Возвращает Id Поздрика по данным друга.")]
    [SwaggerRequestExample(typeof(FriendDto), typeof(GetPozdIdInTgExample))]
    [SwaggerResponse(200, "Id Поздрика найден", typeof(PozdrikIdDto))]
    [SwaggerResponse(404, "Поздравление не найдено")]
    public async Task<IActionResult> GetPozdrikIdForMail([FromBody] FriendDto friend)
    {
        PozdrikIdDto id = await this.mailBotService.GetPozdrikId(friend).ConfigureAwait(false);
        return id.PozdrikId != null ? this.Ok(id) : this.NotFound(new { message = "Поздравление не найдено" });
    }
}
