using Domain.Models;
using Entities.Templates;

namespace Domain.Interfaces.IServices;

public interface IMailBotService
{
    Task<List<FriendList>> SelectForTodayBithrday();
    
    Task<Pozdrik> SelectPozdrikAsync(PozdrikIdDto pozdrikId);
}