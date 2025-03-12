using Domain.Models;
using Entities.Templates;

namespace Domain.Interfaces.IServices;

public interface IMailBotService
{
    Task<List<FriendList>> SelectForTodayBithrday();
    
    Task<string?> SelectPozdStringAsync(PozdrikIdDto pozdrikId);
    
    Task<string?> GetEmailAsync(AppIdDto appId);
}