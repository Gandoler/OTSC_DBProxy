using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;

namespace ProxyAPILeval;

[ApiController]
[Route("api/neirogen")]
public class NeiroGenController : ControllerBase
{
    private readonly INeiroGenService _neiroGenService;

    public NeiroGenController(INeiroGenService neiroGenService)
    {
        _neiroGenService = neiroGenService;
    }

    [HttpGet("pozdrik/{pozdrikId:int}")]
    public async Task<IActionResult> GetIntAndPozh(int pozdrikId)
    {
        var pozdrik = await _neiroGenService.SelectIntAndPozhAsync(new PozdrikIdDto { _pozdrikId = pozdrikId });
        return Ok(pozdrik);
    }

    [HttpPost("pozdrik/add")]
    public async Task<IActionResult> AddPozdrik([FromBody] PozdrStringDTO pozdrikDto)
    {
        var result = await _neiroGenService.AddPozdrAsync(pozdrikDto);
        return result ? Ok(new { message = "Pozdrik added successfully" }) : BadRequest(new { message = "Failed to add pozdrik" });
    }
    [HttpGet("pozdrik/name/{pozdrikId:int}")]
    public async Task<IActionResult> GetNameByPozdrikId(int pozdrikId)
    {
        var name = await _neiroGenService.GetNameByPozdrikId(new PozdrikIdDto { _pozdrikId = pozdrikId });
        return name != null ? Ok(name) : NotFound(new { message = "Name not found" });
    }

    [HttpGet("pozdrik/username/{pozdrikId:int}")]
    public async Task<IActionResult> GetUserNameByPozdrikId(int pozdrikId)
    {
        var username = await _neiroGenService.GetUserNameByPozdrikId(new PozdrikIdDto { _pozdrikId = pozdrikId });
        return username != null ? Ok(username) : NotFound(new { message = "Username not found" });
    }
}