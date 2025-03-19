using Domain.DTO.DTO.Friend;
using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using ProxyAPILeval.DTOExample;
using ProxyAPILeval.DTOExample.ForgotPassword;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

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

    /// <summary>
    /// Получить ID пользователя по email.
    /// </summary>
    [HttpGet("getid/{email}")]
    [SwaggerOperation(Summary = "Получить ID по email", Description = "Возвращает ID пользователя по его email.")]
    [SwaggerResponse(200, "ID успешно получен")]
    [SwaggerResponse(404, "Пользователь не найден")]
    public async Task<IActionResult> GetIdByEmail(string email)
    {
        var result = await _passwordRecoveryService.GetIdByMailAsync(email);
        return Ok(result);
    }

    /// <summary>
    /// Проверить существование почты.
    /// </summary>
    [HttpGet("check-mail/{mail}")]
    [SwaggerOperation(Summary = "Проверить почту", Description = "Проверяет, существует ли пользователь с указанной почтой." +
                                                                 "\n\n (example@example.com) - пример" +
                                                                 "\nselect * FROM mail_comprehensions")]
    [SwaggerResponse(200, "Почта проверена успешно")]
    public async Task<IActionResult> CheckMail(string mail)
    {
        var result = await _passwordRecoveryService.ExistByMailAsync(mail);
        return Ok(result);
    }

    /// <summary>
    /// Проверить существование пользователя по логину.
    /// </summary>
    [HttpGet("exists/{login}")]
    [SwaggerOperation(Summary = "Проверить существование пользователя", Description = "Проверяет, существует ли пользователь с указанным логином.")]
    [SwaggerResponse(200, "Проверка выполнена успешно")]
    public async Task<IActionResult> CheckUserExists(string login)
    {
        var exists = await _passwordRecoveryService.ExicstCheckByLoginAsync(login);
        return Ok(new { exists });
    }

    /// <summary>
    /// Обновить данные пользователя.
    /// </summary>
    [HttpPut("update")]
    [SwaggerOperation(Summary = "Обновить пароль пользователя", Description = "Обновляет пароль пользователя на основе данных из LoginDto.")]
    [SwaggerRequestExample(typeof(FriendDto), typeof(ChangePasswordExample))]
    [SwaggerResponse(200, "Пароль успешно обновлён")]
    [SwaggerResponse(400, "Ошибка при обновлении пароля")]
    public async Task<IActionResult> UpdateUser([FromBody] LoginDto loginDto)
    {
        var result = await _passwordRecoveryService.UpdateAsync(loginDto);
        return result ? Ok(new { message = "Password updated successfully" }) : BadRequest(new { message = "Update failed" });
    }


}

