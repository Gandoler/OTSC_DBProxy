// <copyright file="NeiroGenController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProxyAPILevel.Controllers;

using Domain.DTO.DTO.Pozdr;
using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using ProxyAPILeval.DTOExample;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

[ApiController]
[Route("api/neirogen")]
public class NeiroGenController : ControllerBase
{
    private readonly INeiroGenService neiroGenService;

    public NeiroGenController(INeiroGenService neiroGenService)
    {
        this.neiroGenService = neiroGenService;
    }

    /// <summary>
    /// Получает данные Поздрика по его ID.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpGet("pozdrik/{pozdrikId:int}")]
    [SwaggerOperation(Summary = "Получить данные Поздрика", Description = "Возвращает объект Поздрика по его ID.")]
    [SwaggerResponse(200, "Успешный запрос", typeof(PozdrStringDTO))]
    [SwaggerResponse(404, "Поздрик не найден")]
    public async Task<IActionResult> GetIntAndPozh(int pozdrikId)
    {
        var pozdrik = await this.neiroGenService.SelectIntAndPozhAsync(new PozdrikIdDto { PozdrikId = pozdrikId }).ConfigureAwait(false);
        return this.Ok(pozdrik);
    }

    /// <summary>
    /// Добавляет новый Поздрик.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpPost("pozdrik/add")]
    [SwaggerOperation(Summary = "Добавить Поздрик", Description = "Добавляет новый Поздрик с переданными данными.")]
    [SwaggerRequestExample(typeof(PozdrStringDTO), typeof(PozdrikstringExample))]
    [SwaggerResponse(200, "Поздрик успешно добавлен")]
    [SwaggerResponse(400, "Ошибка при добавлении Поздрика")]
    public async Task<IActionResult> AddPozdrik([FromBody] PozdrStringDTO pozdrikDto)
    {
        var result = await this.neiroGenService.AddPozdrAsync(pozdrikDto).ConfigureAwait(false);
        return result ? this.Ok(new { message = "Pozdrik added successfully" }) : this.BadRequest(new { message = "Failed to add pozdrik" });
    }

    /// <summary>
    /// Получает имя по ID Поздрика.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpGet("pozdrik/name/{pozdrikId:int}")]
    [SwaggerOperation(Summary = "Получить имя Поздрика", Description = "Возвращает имя, связанное с данным Поздриком." +
                                                                       "\n\n 6- пример")]
    [SwaggerResponse(200, "Имя успешно найдено", typeof(string))]
    [SwaggerResponse(404, "Имя не найдено")]
    public async Task<IActionResult> GetNameByPozdrikId(int pozdrikId)
    {
        var name = await this.neiroGenService.GetNameByPozdrikId(new PozdrikIdDto { PozdrikId = pozdrikId }).ConfigureAwait(false);
        return name != null ? this.Ok(name) : this.NotFound(new { message = "Name not found" });
    }

    /// <summary>
    /// Получает имя пользователя по ID Поздрика.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpGet("pozdrik/username/{pozdrikId:int}")]
    [SwaggerOperation(Summary = "Получить имя пользователя", Description = "Возвращает имя пользователя, связанное с данным Поздриком.")]
    [SwaggerResponse(200, "Имя пользователя успешно найдено", typeof(string))]
    [SwaggerResponse(404, "Имя пользователя не найдено")]
    public async Task<IActionResult> GetUserNameByPozdrikId(int pozdrikId)
    {
        var username = await this.neiroGenService.GetUserNameByPozdrikId(new PozdrikIdDto { PozdrikId = pozdrikId }).ConfigureAwait(false);
        return username != null ? this.Ok(username) : this.NotFound(new { message = "Username not found" });
    }

    /*
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
    }*/
}
