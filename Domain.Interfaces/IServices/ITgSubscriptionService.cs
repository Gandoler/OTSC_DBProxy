using Domain.Models;
using Entities.Templates;

namespace Domain.Interfaces.IServices;

public interface ITgSubscriptionService
{
    
    Task<TgComprehension> SubscribeAsync(RegisterTgDto dto);
    
}