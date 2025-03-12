using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;

namespace ProxyAPILeval;

[ApiController]
[Route("api/tgsubscription")]
public class TgSubscriptionController : ControllerBase
{
    private readonly ITgSubscriptionService _tgSubscriptionService;

    public TgSubscriptionController(ITgSubscriptionService tgSubscriptionService)
    {
        _tgSubscriptionService = tgSubscriptionService;
    }

    [HttpPost("subscribe")] 
    public async Task<IActionResult> Subscribe([FromBody] RegisterTgDto dto)
    {
        var result = await _tgSubscriptionService.SubscribeAsync(dto);
        return result ? Ok(new { message = "Subscription successful" }) : BadRequest(new { message = "Subscription failed" });
    }
}