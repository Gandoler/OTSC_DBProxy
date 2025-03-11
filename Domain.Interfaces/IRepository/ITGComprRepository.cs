using Domain.Models;

namespace Domain.Interfaces;

public interface ITgComprRepository
{
    Task<TgComprehension> AddTgAsync(Guid appid, long telegramId);
    Task<Guid> GetIdByTgAsync(long tgId);
    
}