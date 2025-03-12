using Domain.Models;
using Entities.Templates;

namespace Domain.Interfaces.IServices;

public interface ITgSubscriptionService
{
    
    Task<bool> SubscribeAsync(RegisterTgDto dto);
    
}