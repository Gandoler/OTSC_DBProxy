// <copyright file="PasswordRecoveryController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProxyAPILevel;

using Domain.DTO.DTO.Friend;
using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using ProxyAPILeval.DTOExample;
using ProxyAPILeval.DTOExample.ForgotPassword;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

[ApiController]
[Route("api/passwordrecovery")]
public class PasswordRecoveryController : ControllerBase
{
    private readonly IPasswordRecoveryService passwordRecoveryService;

    public PasswordRecoveryController(IPasswordRecoveryService passwordRecoveryService)
    {
        this.passwordRecoveryService = passwordRecoveryService;
    }

    /// <summary>
    /// Получить ID пользователя по email.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpGet("getid/{email}")]
    [SwaggerOperation(Summary = "Получить ID по email", Description = "Возвращает ID пользователя по его email.")]
    [SwaggerResponse(200, "ID успешно получен")]
    [SwaggerResponse(404, "Пользователь не найден")]
    public async Task<IActionResult> GetIdByEmail(string email)
    {
        var result = await this.passwordRecoveryService.GetIdByMailAsync(email).ConfigureAwait(false);
        return this.Ok(result);
    }

    /// <summary>
    /// Получить login пользователя по email.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpGet("getlogin/{email}")]
    [SwaggerOperation(Summary = "Получить login по email", Description = "Возвращает login пользователя по его email.")]
    [SwaggerResponse(200, "login успешно получен")]
    [SwaggerResponse(404, "Пользователь не найден")]
    public async Task<IActionResult> GetLoginByMail(string email)
    {
        var result = await this.passwordRecoveryService.GetLoginByMailAsync(email).ConfigureAwait(false);
        return this.Ok(result);
    }

    /// <summary>
    /// Проверить существование почты.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpGet("check-mail/{mail}")]
    [SwaggerOperation(Summary = "Проверить почту", Description = "Проверяет, существует ли пользователь с указанной почтой." +
                                                                 "\n\n (example@example.com) - пример" +
                                                                 "\nselect * FROM mail_comprehensions")]
    [SwaggerResponse(200, "Почта проверена успешно")]
    public async Task<IActionResult> CheckMail(string mail)
    {
        var result = await this.passwordRecoveryService.ExistByMailAsync(mail).ConfigureAwait(false);
        return this.Ok(result);
    }

    /// <summary>
    /// Проверить существование пользователя по логину.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpGet("exists/{login}")]
    [SwaggerOperation(Summary = "Проверить существование пользователя", Description = "Проверяет, существует ли пользователь с указанным логином.")]
    [SwaggerResponse(200, "Проверка выполнена успешно")]
    public async Task<IActionResult> CheckUserExists(string login)
    {
        var exists = await this.passwordRecoveryService.ExicstCheckByLoginAsync(login).ConfigureAwait(false);
        return this.Ok(new { exists });
    }

    /// <summary>
    /// Обновить данные пользователя.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpPut("update")]
    [SwaggerOperation(Summary = "Обновить пароль пользователя", Description = "Обновляет пароль пользователя на основе данных из LoginDto.")]
    [SwaggerRequestExample(typeof(FriendDto), typeof(ChangePasswordExample))]
    [SwaggerResponse(200, "Пароль успешно обновлён")]
    [SwaggerResponse(400, "Ошибка при обновлении пароля")]
    public async Task<IActionResult> UpdateUser([FromBody] LoginDto loginDto)
    {
        var result = await this.passwordRecoveryService.UpdateAsync(loginDto).ConfigureAwait(false);
        return result ? this.Ok(new { message = "Password updated successfully" }) : this.BadRequest(new { message = "Update failed" });
    }
}
