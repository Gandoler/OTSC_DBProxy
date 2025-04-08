// <copyright file="FriendController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProxyAPILevel;

using Domain.DTO.DTO.Friend;
using Domain.DTO.DTO.Pozdr;
using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using ProxyAPILeval.DTOExample;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

[ApiController]
[Route("api/Friends")]
public class FriendController : ControllerBase
{
    private readonly IFriendService friendService;

    public FriendController(IFriendService friendService)
    {
        this.friendService = friendService;
    }

    // надо

    /// <summary>
    /// Добавляет друга в список.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
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
        var result = await this.friendService.AddFriendInListAsync(friendDto).ConfigureAwait(false);
        return result ? this.Ok(new { message = "Friend added successfully" }) : this.BadRequest(new { message = "Failed to add friend" });
    }

    /// <summary>
    /// Удаляет друга из списка.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpDelete("delete")]
    [SwaggerOperation(Summary = "Удалить друга", Description = "Удаляет друга по AppId и FriendUsername.")]
    [SwaggerRequestExample(typeof(DeleteFriendDto), typeof(DeleteFriendDtoExample))]
    [SwaggerResponse(200, "Друг успешно удалён")]
    [SwaggerResponse(400, "Ошибка при удалении друга")]
    public async Task<IActionResult> DeleteFriend([FromBody] DeleteFriendDto deleteFriendDto)
    {
        var result = await this.friendService.DeleteFriendFromListAsync(deleteFriendDto).ConfigureAwait(false);
        return result ? this.Ok(new { message = "Friend deleted successfully" }) : this.BadRequest(new { message = "Failed to delete friend" });
    }

    /// <summary>
    /// Обновляет информацию о друге.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpPut("update")]
    [SwaggerOperation(Summary = "Обновить друга", Description = "Обновляет данные о друге.")]
    [SwaggerRequestExample(typeof(FriendDto), typeof(FriendUpdateExample))]
    [SwaggerResponse(200, "Данные успешно обновлены")]
    [SwaggerResponse(400, "Ошибка при обновлении данных")]
    public async Task<IActionResult> UpdateFriend([FromBody] FriendDto friendDto)
    {
        var result = await this.friendService.UpdateFriendInListAsync(friendDto).ConfigureAwait(false);
        return result ? this.Ok(new { message = "Friend updated successfully" }) : this.BadRequest(new { message = "Failed to update friend" });
    }

    /// <summary>
    /// Получает список друзей по AppId.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpGet("list/{appId:guid}")]
    [SwaggerOperation(Summary = "Получить список друзей", Description = "Возвращает список друзей по AppId." +
                                                                        "\\n\\n(пример: 550e8400-e29b-41d4-a716-446655440000)]")]
    [SwaggerResponse(200, "Список друзей успешно получен")]
    public async Task<IActionResult> GetFriendsByAppId(Guid appId)
    {
        var friends = await this.friendService.SelectByAppIdAsync(new AppIdDto { AppId = appId }).ConfigureAwait(false);
        return this.Ok(friends);
    }

    /// <summary>
    /// Получает Id Поздрика по имени пользователя и AppId.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpGet("pozdrik/{username}/{appId:guid}")]
    [SwaggerOperation(Summary = "Получить Id Поздрика", Description = "Возвращает Id Поздрика по юзернейму пользователя и AppId." +
                                                                      "/n/n (пример: 550e8400-e29b-41d4-a716-446655440000)" +
                                                                      "\n\n(best_friend123)")]
    [SwaggerResponse(200, "Id Поздрика успешно получен")]
    public async Task<IActionResult> GetPozdrikId(string username, Guid appId)
    {
        var pozdrikIdDto = await this.friendService.GetPozdrikIdAsync(new GetPozdrikQueryDto { Username = username, Appid = appId }).ConfigureAwait(false);
        return this.Ok(pozdrikIdDto);
    }

    /// <summary>
    /// Добавляет интересы и пожелания по Id Поздрика.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpPost("pozdrik/addIntAndWish")]
    [SwaggerOperation(Summary = "Добавить интересы и пожелания", Description = "Добавляет интересы и пожелания по Id Поздрика." +
                                                                               "\n\n SELECT * FROM pozdrik - посмотреть")]
    [SwaggerRequestExample(typeof(AddIntAndPozhDto), typeof(AddIntAndPozhDtoExample))]
    [SwaggerResponse(200, "Данные успешно добавлены")]
    [SwaggerResponse(400, "Ошибка при добавлении данных")]
    public async Task<IActionResult> SetPozdrikIdTOFrined([FromBody] AddIntAndPozhDto intAndPozhDto)
    {
        var result = await this.friendService.AddIntAndPozhAsync(intAndPozhDto).ConfigureAwait(false);
        return result ? this.Ok(new { message = "Data added successfully" }) : this.BadRequest(new { message = "Failed to add data" });
    }

    /// <summary>
    /// Получает интересы и пожелания по Id Поздрика.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpGet("pozdrik/get/{pozdrikId:int}")]
    [SwaggerOperation(Summary = "Получить интересы и пожелания", Description = "Возвращает интересы и пожелания по Id Поздрика.")]
    [SwaggerResponse(200, "Интересы и пожелания успешно получены")]
    public async Task<IActionResult> GetIntAndPozh(int pozdrikId)
    {
        var result = await this.friendService.SelectIntAndPozhAsync(new PozdrikIdDto { PozdrikId = pozdrikId }).ConfigureAwait(false);
        return this.Ok(result);
    }

    // надо

    /// <summary>
    /// Добавить запись с поздравнием.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpPost("SetpozdrId")]
    [SwaggerOperation(Summary = "Добавить id поздрик", Description = "Добавляет Id Поздрика." +
                                                                     "SELECT * FROM pozdrik")]
    [SwaggerRequestExample(typeof(AddPozdrIdDto), typeof(SetPozdrIdExample))]
    [SwaggerResponse(200, "Интересы и пожелания успешно добавлены")]
    [SwaggerResponse(400, "Ошибка при добавлении интересов и пожеланий")]
    public async Task<IActionResult> SetPozdrikIdTOFriend([FromBody] AddPozdrIdDto dto)
    {
        var result = await this.friendService.AddPozdrikIdToFriendAsync(dto).ConfigureAwait(false);
        if (dto.PozdrikId == 0)
        {
            throw new Exception("pozdrId is invalid");
        }

        return result ? this.Ok(new { message = "поздравление успешно привязано" }) : this.BadRequest(new { message = "Ошибка при привязке таблицы поздравления" });
    }

    // надо

    /// <summary>
    /// Создает новую запись Поздрика.
    /// </summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpPost("pozdrik/create")]
    [SwaggerOperation(Summary = "Создать Поздрик", Description = "Создает новую запись Поздрика с интересами и пожеланиями.")]
    [SwaggerResponse(200, "Поздрик успешно создан", typeof(int))]
    [SwaggerResponse(400, "Ошибка при создании Поздрика")]
    public async Task<IActionResult> CreatePozdrik([FromBody] AddIntAndPozhDto dto)
    {
        PozdrikIdDto pozdrikId = await this.friendService.CreatePozdrikAsync(dto.Interests, dto.Pozhelania).ConfigureAwait(false);
        if (pozdrikId.PozdrikId == null)
        {
            return this.BadRequest(new { message = "Ошибка при создании Поздрика: ID не получен" });
        }

        return pozdrikId.PozdrikId != null
            ? this.Ok(new { message = "Поздрик успешно создан", pozdrikId })
            : this.BadRequest(new { message = "Ошибка при создании Поздрика" });
    }

    [HttpPost("pozdrik/addWithWish")]
    public async Task<IActionResult> AddIntAndWish([FromBody] AddFriendWithWishDTO dto)
    {
        var result = await this.friendService.AddFriendAndWishAsync(dto.Friend, dto.Pozh).ConfigureAwait(false);
        return result ? this.Ok(new { message = "Friend added successfully" }) : this.BadRequest(new { message = "Failed to add friend" });
    }
}
