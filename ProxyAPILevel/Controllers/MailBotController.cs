using Domain.DTO.DTO.Friend;
using Domain.DTO.DTO.Pozdr;
using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using ProxyAPILeval.DTOExample;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

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

    /// <summary>
    /// Получает список друзей с днями рождения сегодня.
    /// </summary>
    [HttpGet("birthdays/today")]
    [SwaggerOperation(Summary = "Получить сегодняшние дни рождения", Description = "Возвращает список друзей, у которых сегодня день рождения.")]
    [SwaggerResponse(200, "Список успешно получен")]
    public async Task<IActionResult> GetTodayBirthdays()
    {
        var friends = await _mailBotService.SelectForTodayBithrday();
        return Ok(friends);
    }

    /// <summary>
    /// Получает строку поздравления по ID.
    /// </summary>
    [HttpGet("GetCongrString/{pozdrikId:int}")]
    [SwaggerOperation(Summary = "Получить поздравление", Description = "Возвращает поздравительную строку по указанному ID Поздрика.")]
    [SwaggerResponse(200, "Поздравление успешно найдено")]
    [SwaggerResponse(404, "Поздравление не найдено")]
    public async Task<IActionResult> GetCongrStr(int pozdrikId)
    {
        var pozdrik = await _mailBotService.SelectPozdStringAsync(new PozdrikIdDto { _pozdrikId = pozdrikId });
        return pozdrik != null ? Ok(pozdrik) : NotFound(new { message = "Pozdrik not found" });
    }

    /// <summary>
    /// Получает email по AppId.
    /// </summary>
    [HttpGet("email/{appId:guid}")]
    [SwaggerOperation(Summary = "Получить email", Description = "Возвращает email по указанному AppId." +
                                                                "\n\n(550e8400-e29b-41d4-a716-446655440000) - пример")]
    [SwaggerResponse(200, "Email успешно найден")]
    [SwaggerResponse(404, "Email не найден")]
    public async Task<IActionResult> GetEmail(Guid appId)
    {
        var email = await _mailBotService.GetEmailAsync(new AppIdDto { AppId = appId });
        return email != null ? Ok(email) : NotFound(new { message = "Email not found" });
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
        PozdrikIdDto id = await _mailBotService.GetPozdrikId(friend);
        return id != null ? Ok(id) : NotFound(new { message = "Поздравление не найдено" });
    }
}
