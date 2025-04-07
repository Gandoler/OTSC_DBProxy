using Domain.DTO.DTO.Friend;
using Domain.DTO.DTO.MailComp;
using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using ProxyAPILeval.DTOExample;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace ProxyAPILevel;

[ApiController]
[Route("api/register")]
public class RegisterController : ControllerBase
{
    private readonly IRegistrService _registerService;

    public RegisterController(IRegistrService registerService)
    {
        _registerService = registerService;
    }

    /// <summary>
    /// Регистрирует нового пользователя.
    /// </summary>
    [HttpPost("create")] 
    [SwaggerOperation(Summary = "Регистрация пользователя", Description = "Регистрирует нового пользователя с логином, паролем и email.")]
    [SwaggerRequestExample(typeof(RegisterDto), typeof(RegisterInAppExample))]
    [SwaggerResponse(200, "Пользователь успешно зарегистрирован")]
    [SwaggerResponse(400, "Ошибка при регистрации")] 
    public async Task<IActionResult> RegisterUser([FromBody] RegisterDto dto)
    {
        Guid? result = await _registerService.RegisterAsync(dto);
        if (!(result is null))
        {
            return Ok(result);
        }
        else return BadRequest();
    }

    /// <summary>
    /// Проверяет существование пользователя по email.
    /// </summary>
    [HttpGet("exists/{login}")] 
    [SwaggerOperation(Summary = "Проверка существования пользователя", Description = "Проверяет, существует ли пользователь с указанным email.")]
    
    [SwaggerResponse(200, "Пользователь существует")]
    [SwaggerResponse(400, "Пользователь не существует")]
    public async Task<IActionResult> CheckUserExists( string login)
    {
        var exists = await _registerService.ExicstCheckAsync(new CheckExistDto(){Email = login});//ха-ха-ха
        return exists ? Ok(new { message = "User exists" }) : BadRequest(new { message = "User does not exist" });
    }

    /// <summary>
    /// Получает ID пользователя по логину.
    /// </summary>
    [HttpGet("UserId/{userlogin}")] 
    [SwaggerOperation(Summary = "Получить ID пользователя", Description = "Возвращает AppId пользователя по логину." +
                                                                          "\n\n(admin)-пример")]
    
    [SwaggerResponse(200, "AppId успешно найден", typeof(AppIdDto))]
    [SwaggerResponse(400, "Пользователь не найден")]
    public async Task<IActionResult> GetUserIdByLogin(string userlogin)
    {
        AppIdDto? appIdDto = await _registerService.GetAppId(new CheckExistDto{Email = userlogin});
        if (appIdDto is null) return BadRequest(new { message = "User does not exist" });
        return Ok(appIdDto);
    }

    /// <summary>
    /// Добавляет почту пользователю.
    /// </summary>
    [HttpPut("Mail")] 
    [SwaggerOperation(Summary = "Добавить email", Description = "Добавляет email для существующего пользователя по AppId.")]
    [SwaggerRequestExample(typeof(ADDMailDto), typeof(ADDMaiExample))]
    [SwaggerResponse(200, "Email успешно добавлен")]
    [SwaggerResponse(400, "Email уже существует")]
    public async Task<IActionResult> AddMail([FromBody] ADDMailDto dto)
    {
        bool add = await _registerService.AddMail(dto);
        if (add) return Ok(new { message = "Email added successfully" });
        return BadRequest(new { message = "Mail already exists" });
    }
}
