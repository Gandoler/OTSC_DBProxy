// <copyright file="RegisterController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProxyAPILevel;

using Domain.DTO.DTO.Friend;
using Domain.DTO.DTO.MailComp;
using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using ProxyAPILeval.DTOExample;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

[ApiController]
[Route("api/register")]
public class RegisterController : ControllerBase
{
    private readonly IRegistrService registerService;

    public RegisterController(IRegistrService registerService)
    {
        this.registerService = registerService;
    }

    /// <summary>
    /// Регистрирует нового пользователя.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpPost("create")]
    [SwaggerOperation(Summary = "Регистрация пользователя", Description = "Регистрирует нового пользователя с логином, паролем и email.")]
    [SwaggerRequestExample(typeof(RegisterDto), typeof(RegisterInAppExample))]
    [SwaggerResponse(200, "Пользователь успешно зарегистрирован")]
    [SwaggerResponse(400, "Ошибка при регистрации")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterDto dto)
    {
        Guid? result = await this.registerService.RegisterAsync(dto).ConfigureAwait(false);
        if (!(result is null))
        {
            return this.Ok(result);
        }
        else
        {
            return this.BadRequest();
        }
    }

    /// <summary>
    /// Проверяет существование пользователя по email.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpGet("exists/{login}")]
    [SwaggerOperation(Summary = "Проверка существования пользователя", Description = "Проверяет, существует ли пользователь с указанным email.")]

    [SwaggerResponse(200, "Пользователь существует")]
    [SwaggerResponse(400, "Пользователь не существует")]
    public async Task<IActionResult> CheckUserExists(string login)
    {
        var exists = await this.registerService.ExicstCheckAsync(new CheckExistDto() { Email = login }).ConfigureAwait(false); // ха-ха-ха
        return exists ? this.Ok(new { message = "User exists" }) : this.BadRequest(new { message = "User does not exist" });
    }

    /// <summary>
    /// Получает ID пользователя по логину.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpGet("UserId/{userlogin}")]
    [SwaggerOperation(Summary = "Получить ID пользователя", Description = "Возвращает AppId пользователя по логину." +
                                                                          "\n\n(admin)-пример")]

    [SwaggerResponse(200, "AppId успешно найден", typeof(AppIdDto))]
    [SwaggerResponse(400, "Пользователь не найден")]
    public async Task<IActionResult> GetUserIdByLogin(string userlogin)
    {
        AppIdDto? appIdDto = await this.registerService.GetAppId(new CheckExistDto { Email = userlogin }).ConfigureAwait(false);
        if (appIdDto is null)
        {
            return this.BadRequest(new { message = "User does not exist" });
        }

        return this.Ok(appIdDto);
    }

    /// <summary>
    /// Добавляет почту пользователю.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpPut("Mail")]
    [SwaggerOperation(Summary = "Добавить email", Description = "Добавляет email для существующего пользователя по AppId.")]
    [SwaggerRequestExample(typeof(ADDMailDto), typeof(ADDMaiExample))]
    [SwaggerResponse(200, "Email успешно добавлен")]
    [SwaggerResponse(400, "Email уже существует")]
    public async Task<IActionResult> AddMail([FromBody] ADDMailDto dto)
    {
        bool add = await this.registerService.AddMail(dto).ConfigureAwait(false);
        if (add)
        {
            return this.Ok(new { message = "Email added successfully" });
        }

        return this.BadRequest(new { message = "Mail already exists" });
    }
}
