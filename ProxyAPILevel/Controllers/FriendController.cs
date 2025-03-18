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
[Route("api/Friends")]
public class FriendController : ControllerBase
{
    private readonly IFriendService _friendService;

    public FriendController(IFriendService friendService)
    {
        _friendService = friendService;
    }

    /// <summary>
    /// Добавляет друга в список.
    /// </summary>
    [HttpPost("add")]
    [SwaggerOperation(Summary = "Добавить друга", Description = "Добавляет друга в список друзей по AppId." +
                                                                "\n\nSELECT * FROM friend_list для просмотра" +
                                                                "\n\n для делита DELETE  FROM friend_list\nWHERE appid = '550e8400-e29b-41d4-a716-446655440000' " +
                                                                "and friend_username = 'best_friend123'")]
    [SwaggerRequestExample(typeof(FriendDto), typeof(FriendDtoExample))]
    [SwaggerResponse(200, "Друг успешно добавлен")]
    [SwaggerResponse(400, "Ошибка при добавлении друга")]
    public async Task<IActionResult> AddFriend([FromBody] FriendDto friendDto)
    {
        var result = await _friendService.AddFriendInListAsync(friendDto);
        return result ? Ok(new { message = "Friend added successfully" }) : BadRequest(new { message = "Failed to add friend" });
    }

    /// <summary>
    /// Удаляет друга из списка.
    /// </summary>
    [HttpDelete("delete")]
    [SwaggerOperation(Summary = "Удалить друга", Description = "Удаляет друга по AppId и FriendUsername.")]
    [SwaggerRequestExample(typeof(DeleteFriendDto), typeof(DeleteFriendDtoExample))]
    [SwaggerResponse(200, "Друг успешно удалён")]
    [SwaggerResponse(400, "Ошибка при удалении друга")]
    public async Task<IActionResult> DeleteFriend([FromBody] DeleteFriendDto deleteFriendDto)
    {
        var result = await _friendService.DeleteFriendFromListAsync(deleteFriendDto);
        return result ? Ok(new { message = "Friend deleted successfully" }) : BadRequest(new { message = "Failed to delete friend" });
    }

    /// <summary>
    /// Обновляет информацию о друге.
    /// </summary>
    [HttpPut("update")]
    [SwaggerOperation(Summary = "Обновить друга", Description = "Обновляет данные о друге.")]
    [SwaggerRequestExample(typeof(FriendDto), typeof(FriendUpdateExample))]
    [SwaggerResponse(200, "Данные успешно обновлены")]
    [SwaggerResponse(400, "Ошибка при обновлении данных")]
    public async Task<IActionResult> UpdateFriend([FromBody] FriendDto friendDto)
    {
        var result = await _friendService.UpdateFriendInListAsync(friendDto);
        return result ? Ok(new { message = "Friend updated successfully" }) : BadRequest(new { message = "Failed to update friend" });
    }

    /// <summary>
    /// Получает список друзей по AppId.
    /// </summary>
    [HttpGet("list/{appId:guid}")]
    [SwaggerOperation(Summary = "Получить список друзей", Description = "Возвращает список друзей по AppId." +
                                                                        "\\n\\n(пример: 550e8400-e29b-41d4-a716-446655440000)]")]
    [SwaggerResponse(200, "Список друзей успешно получен")]
    public async Task<IActionResult> GetFriendsByAppId(Guid appId)
    {
        var friends = await _friendService.SelectByAppIdAsync(new AppIdDto { AppId = appId });
        return Ok(friends);
    }

    /// <summary>
    /// Получает Id Поздрика по имени пользователя и AppId.
    /// </summary>
    [HttpGet("pozdrik/{username}/{appId:guid}")]
    [SwaggerOperation(Summary = "Получить Id Поздрика", Description = "Возвращает Id Поздрика по юзернейму пользователя и AppId." +
                                                                      "/n/n (пример: 550e8400-e29b-41d4-a716-446655440000)" +
                                                                      "\n\n(best_friend123)")]
    [SwaggerResponse(200, "Id Поздрика успешно получен")]
    public async Task<IActionResult> GetPozdrikId(string username, Guid appId)
    {
        var pozdrikIdDto = await _friendService.GetPozdrikIdAsync(new GetPozdrikQueryDto { Username = username, Appid = appId });
        return Ok(pozdrikIdDto);
    }

    /// <summary>
    /// Добавляет интересы и пожелания по Id Поздрика.
    /// </summary>
    [HttpPost("pozdrik/add")]
    [SwaggerOperation(Summary = "Добавить интересы и пожелания", Description = "Добавляет интересы и пожелания по Id Поздрика.")]
    [SwaggerRequestExample(typeof(AddIntAndPozhDto), typeof(AddIntAndPozhDtoExample))]
    [SwaggerResponse(200, "Данные успешно добавлены")]
    [SwaggerResponse(400, "Ошибка при добавлении данных")]
    public async Task<IActionResult> AddIntAndPozh([FromBody] AddIntAndPozhDto intAndPozhDto)
    {
        var result = await _friendService.AddIntAndPozhAsync(intAndPozhDto);
        return result ? Ok(new { message = "Data added successfully" }) : BadRequest(new { message = "Failed to add data" });
    }

    /// <summary>
    /// Получает интересы и пожелания по Id Поздрика.
    /// </summary>
    [HttpGet("pozdrik/get/{pozdrikId:int}")]
    [SwaggerOperation(Summary = "Получить интересы и пожелания", Description = "Возвращает интересы и пожелания по Id Поздрика.")]
    [SwaggerResponse(200, "Интересы и пожелания успешно получены")]
    public async Task<IActionResult> GetIntAndPozh(int pozdrikId)
    {
        var result = await _friendService.SelectIntAndPozhAsync(new PozdrikIdDto { _pozdrikId = pozdrikId });
        return Ok(result);
    }
    
    
    /// <summary>
    /// Добавить запись с поздравнием.
    /// </summary>    
    [HttpPost("SetpozdrId")]
    [SwaggerOperation(Summary = "Добавить id поздрик", Description = "Добавляет Id Поздрика." +
                                                                     "SELECT * FROM pozdrik")]
    [SwaggerRequestExample(typeof(AddPozdrIdDto), typeof(SetPozdrIdExample))]
    [SwaggerResponse(200, "Интересы и пожелания успешно добавлены")]
    [SwaggerResponse(400, "Ошибка при добавлении интересов и пожеланий")]
    public async Task<IActionResult> AddIntAndPozh([FromBody] AddPozdrIdDto dto )
    {
        var result = await _friendService.AddPozdrikIdToFriendAsync(dto);
        return result ? Ok(new { message = "поздравление успешно привязано" }) : BadRequest(new { message = "Ошибка при привязке таблицы поздравления" });
    }
}
