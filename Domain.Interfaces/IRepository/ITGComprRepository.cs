using Domain.Models;

namespace Domain.Interfaces;

public interface ITgComprRepository
{
    Task<bool> AddTgAsync(Guid appid, long telegramId);
    Task<Guid> GetIdByTgAsync(long tgId);
    
    Task<long?> GetTgId(Guid appId);
}