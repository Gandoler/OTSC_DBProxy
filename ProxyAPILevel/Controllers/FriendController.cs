using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;

namespace ProxyAPILevel;
[ApiController]
[Route("api/Friends")]
public class FriendController: ControllerBase
{
    private readonly IFriendService _friendService;

    public FriendController(IFriendService friendService)
    {
        _friendService = friendService;
    }
    
    [HttpPost("add")]//
    public async Task<IActionResult> AddFriend([FromBody] FriendDto friendDto)
    {
        var result = await _friendService.AddFriendInListAsync(friendDto);
        return result ? Ok(new { message = "Friend added successfully" }) : BadRequest(new { message = "Failed to add friend" });
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteFriend([FromBody] DeleteFriendDto deleteFriendDto)
    {
        var result = await _friendService.DeleteFriendFromListAsync(deleteFriendDto);
        return result ? Ok(new { message = "Friend deleted successfully" }) : BadRequest(new { message = "Failed to delete friend" });
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateFriend([FromBody] FriendDto friendDto)
    {
        var result = await _friendService.UpdateFriendInListAsync(friendDto);
        return result ? Ok(new { message = "Friend updated successfully" }) : BadRequest(new { message = "Failed to update friend" });
    }

    [HttpGet("list/{appId:guid}")]
    public async Task<IActionResult> GetFriendsByAppId(Guid appId)
    {
        var friends = await _friendService.SelectByAppIdAsync(new AppIdDto { AppId = appId });
        return Ok(friends);
    }

    [HttpGet("pozdrik/{username}/{appId:guid}")]
    public async Task<IActionResult> GetPozdrikId(string username, Guid appId)
    {
        var pozdrikIdDto = await _friendService.GetPozdrikIdAsync(new GetPozdrikQueryDto { Username = username, Appid = appId });
        return Ok(pozdrikIdDto);
    }

    [HttpPost("pozdrik/add")]
    public async Task<IActionResult> AddIntAndPozh([FromBody] AddIntAndPozhDto intAndPozhDto)
    {
        var result = await _friendService.AddIntAndPozhAsync(intAndPozhDto);
        return result ? Ok(new { message = "Data added successfully" }) : BadRequest(new { message = "Failed to add data" });
    }

    [HttpGet("pozdrik/get/{pozdrikId:int}")]
    public async Task<IActionResult> GetIntAndPozh(int pozdrikId)
    {
        var result = await _friendService.SelectIntAndPozhAsync(new PozdrikIdDto { _pozdrikId = pozdrikId });
        return Ok(result);
    }
}