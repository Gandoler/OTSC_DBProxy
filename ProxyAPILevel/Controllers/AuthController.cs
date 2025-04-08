// <copyright file="AuthController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProxyAPILevel;

using System.Threading.Tasks;
using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using ProxyAPILeval.DTOExample;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

[ApiController]
[Route("api/Auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    /// <summary>
    /// Проверяет, существует ли пользователь с переданными данными.
    /// </summary>
    /// <param name="loginDto">Логин и пароль пользователя.</param>
    /// <returns>Флаг существования пользователя.</returns>
    [HttpPost("login")]
    [SwaggerOperation(
        Summary = "Проверка существования пользователя",
        Description = "Отправьте логин и пароль для проверки существования пользователя." +
                      "\n\n SELECT * FROM users что бы посмотреть в бд")]
    [SwaggerRequestExample(typeof(LoginDto), typeof(LoginDtoExample))]
    [SwaggerResponse(200, "Пользователь найден или не найден", typeof(object))]
    public async Task<IActionResult> CheckUserExists([FromBody] LoginDto loginDto)
    {
        bool exists = await this.authService.ExicstCheckAsync(loginDto).ConfigureAwait(false);
        return this.Ok(new { exists });
    }
}
