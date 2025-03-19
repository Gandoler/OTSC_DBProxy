using Domain.DTO.DTO.Pozdr;
using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using ProxyAPILeval.DTOExample;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace ProxyAPILevel.Controllers;

[ApiController]
[Route("api/neirogen")]
public class NeiroGenController : ControllerBase
{
    private readonly INeiroGenService _neiroGenService;

    public NeiroGenController(INeiroGenService neiroGenService)
    {
        _neiroGenService = neiroGenService;
    }

    /// <summary>
    /// Получает данные Поздрика по его ID.
    /// </summary>
    [HttpGet("pozdrik/{pozdrikId:int}")]
    [SwaggerOperation(Summary = "Получить данные Поздрика", Description = "Возвращает объект Поздрика по его ID.")]
    [SwaggerResponse(200, "Успешный запрос", typeof(PozdrStringDTO))]
    [SwaggerResponse(404, "Поздрик не найден")]
    public async Task<IActionResult> GetIntAndPozh(int pozdrikId)
    {
        var pozdrik = await _neiroGenService.SelectIntAndPozhAsync(new PozdrikIdDto { _pozdrikId = pozdrikId });
        return Ok(pozdrik);
    }

    /// <summary>
    /// Добавляет новый Поздрик.
    /// </summary>
    [HttpPost("pozdrik/add")]
    [SwaggerOperation(Summary = "Добавить Поздрик", Description = "Добавляет новый Поздрик с переданными данными.")]
    [SwaggerRequestExample(typeof(PozdrStringDTO), typeof(PozdrikstringExample))]
    [SwaggerResponse(200, "Поздрик успешно добавлен")]
    [SwaggerResponse(400, "Ошибка при добавлении Поздрика")]
    public async Task<IActionResult> AddPozdrik([FromBody] PozdrStringDTO pozdrikDto)
    {
        var result = await _neiroGenService.AddPozdrAsync(pozdrikDto);
        return result ? Ok(new { message = "Pozdrik added successfully" }) : BadRequest(new { message = "Failed to add pozdrik" });
    }

    /// <summary>
    /// Получает имя по ID Поздрика.
    /// </summary>
    [HttpGet("pozdrik/name/{pozdrikId:int}")]
    [SwaggerOperation(Summary = "Получить имя Поздрика", Description = "Возвращает имя, связанное с данным Поздриком." +
                                                                       "\n\n 6- пример")]
    [SwaggerResponse(200, "Имя успешно найдено", typeof(string))]
    [SwaggerResponse(404, "Имя не найдено")]
    public async Task<IActionResult> GetNameByPozdrikId(int pozdrikId)
    {
        var name = await _neiroGenService.GetNameByPozdrikId(new PozdrikIdDto { _pozdrikId = pozdrikId });
        return name != null ? Ok(name) : NotFound(new { message = "Name not found" });
    }

    /// <summary>
    /// Получает имя пользователя по ID Поздрика.
    /// </summary>
    [HttpGet("pozdrik/username/{pozdrikId:int}")]
    [SwaggerOperation(Summary = "Получить имя пользователя", Description = "Возвращает имя пользователя, связанное с данным Поздриком.")]
    [SwaggerResponse(200, "Имя пользователя успешно найдено", typeof(string))]
    [SwaggerResponse(404, "Имя пользователя не найдено")]
    public async Task<IActionResult> GetUserNameByPozdrikId(int pozdrikId)
    {
        var username = await _neiroGenService.GetUserNameByPozdrikId(new PozdrikIdDto { _pozdrikId = pozdrikId });
        return username != null ? Ok(username) : NotFound(new { message = "Username not found" });
    }

    /// <summary>
    /// Получает Поздрики с пустыми или нулевыми строками.
    /// </summary>
    [HttpGet("pozdrik/emptyornull")]
    [SwaggerOperation(Summary = "Получить пустые или нулевые Поздрики", Description = "Возвращает список Поздриков с пустыми или нулевыми строками.")]
    [SwaggerResponse(200, "Поздрики успешно получены", typeof(IEnumerable<PozdrStringDTO>))]
    [SwaggerResponse(404, "Поздрики не найдены")]
    public async Task<IActionResult> GetEmptyOrNullPozdrstring()
    {
        var result = await _neiroGenService.SelectEmptyOrNullPozdrstringAsync();
        return result.Any() ? Ok(result) : NotFound(new { message = "No empty or null pozdrstrings found" });
    }
}
