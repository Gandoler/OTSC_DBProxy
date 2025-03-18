using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using ProxyAPILeval.DTOExample;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace ProxyAPILevel;

[ApiController]
[Route("api/tgsubscription")]
public class TgSubscriptionController : ControllerBase
{
    private readonly ITgSubscriptionService _tgSubscriptionService;

    public TgSubscriptionController(ITgSubscriptionService tgSubscriptionService)
    {
        _tgSubscriptionService = tgSubscriptionService;
    }

    /// <summary>
    /// Подписывает пользователя на Telegram-уведомления.
    /// </summary>
    /// <param name="dto">Данные для регистрации</param>
    /// <returns>Результат операции</returns>
    [HttpPut("subscribe")]
    [SwaggerOperation(
        Summary = "Подписка на Telegram-уведомления",
        Description = "Позволяет пользователю подписаться на уведомления в Telegram, используя предоставленные данные."
    )]
    [SwaggerRequestExample(typeof(RegisterTgDto), typeof(RegisterTgDtoExample))]
    [SwaggerResponseExample(200, typeof(SuccessfulSubscriptionExample))]
    [SwaggerResponseExample(400, typeof(FailedSubscriptionExample))]
    [SwaggerResponse(200, "Подписка успешно оформлена", typeof(object))]
    [SwaggerResponse(400, "Ошибка при оформлении подписки", typeof(object))]
    public async Task<IActionResult> Subscribe([FromBody] RegisterTgDto dto)
    {
        var result = await _tgSubscriptionService.SubscribeAsync(dto);
        return result 
            ? Ok(new { message = "Подписка успешно оформлена" }) 
            : BadRequest(new { message = "Ошибка при оформлении подписки" });
    }
}