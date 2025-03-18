using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Threading.Tasks;
using ProxyAPILeval.DTOExample;

namespace ProxyAPILevel;

[ApiController]
[Route("api/Auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Проверяет, существует ли пользователь с переданными данными.
    /// </summary>
    /// <param name="loginDto">Логин и пароль пользователя</param>
    /// <returns>Флаг существования пользователя</returns>
    [HttpPost("login")]
    [SwaggerOperation(Summary = "Проверка существования пользователя", 
        Description = "Отправьте логин и пароль для проверки существования пользователя." +
                      "\n\n SELECT * FROM users что бы посмотреть в бд")]
    [SwaggerRequestExample(typeof(LoginDto), typeof(LoginDtoExample))]
    [SwaggerResponse(200, "Пользователь найден или не найден", typeof(object))]
    public async Task<IActionResult> CheckUserExists([FromBody] LoginDto loginDto)
    {
        bool exists = await _authService.ExicstCheckAsync(loginDto);
        return Ok(new { exists });
    }
}