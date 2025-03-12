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
}