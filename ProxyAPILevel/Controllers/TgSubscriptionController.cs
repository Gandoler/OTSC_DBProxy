// <copyright file="TgSubscriptionController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProxyAPILevel;

using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using ProxyAPILeval.DTOExample;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

[ApiController]
[Route("api/tgsubscription")]
public class TgSubscriptionController : ControllerBase
{
    private readonly ITgSubscriptionService tgSubscriptionService;

    public TgSubscriptionController(ITgSubscriptionService tgSubscriptionService)
    {
        this.tgSubscriptionService = tgSubscriptionService;
    }

    /// <summary>
    /// Подписывает пользователя на Telegram-уведомления.
    /// </summary>
    /// <param name="dto">Данные для регистрации.</param>
    /// <returns>Результат операции.</returns>
    [HttpPut("subscribe")]
    [SwaggerOperation(
        Summary = "Подписка на Telegram-уведомления",
        Description = "Позволяет пользователю подписать свой Telegram, используя предоставленные данные." +
                      "\n\nдля проверки SELECT * FROM tg_comprehensions" +
                      "\n\n для удаления DELETE FROM tg_comprehensions\nWHERE appid = '550e8400-e29b-41d4-a716-446655440000'")]
    [SwaggerRequestExample(typeof(RegisterTgDto), typeof(RegisterTgDtoExample))]
    [SwaggerResponseExample(200, typeof(SuccessfulSubscriptionExample))]
    [SwaggerResponseExample(400, typeof(FailedSubscriptionExample))]
    [SwaggerResponse(200, "Подписка успешно оформлена", typeof(object))]
    [SwaggerResponse(400, "Ошибка при оформлении подписки", typeof(object))]
    public async Task<IActionResult> Subscribe([FromBody] RegisterTgDto dto)
    {
        var result = await this.tgSubscriptionService.SubscribeAsync(dto).ConfigureAwait(false);
        return result
            ? this.Ok(new { message = "Подписка успешно оформлена" })
            : this.BadRequest(new { message = "Ошибка при оформлении подписки" });
    }
}
