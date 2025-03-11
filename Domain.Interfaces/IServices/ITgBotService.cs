using Domain.Models;

namespace Domain.Interfaces.IServices;

public interface ITgBotService
{
    Task<List<FriendList>> SelectForTodayBithrday();
    
    Task<Pozdrik> SelectPozdrikAsync(int idPozdr);
}