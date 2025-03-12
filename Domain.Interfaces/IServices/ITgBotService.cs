using Domain.Models;
using Entities.Templates;

namespace Domain.Interfaces.IServices;

public interface ITgBotService
{
    Task<List<FriendList>> SelectForTodayBithrdayAsync();
    
    Task<long?> GetTgIdAsync(AppIdDto appId);
    Task<string?> SelectPozdrStringAsync(PozdrikIdDto pozdrikId);
}