using Domain.Models;

namespace Domain.Interfaces;

public interface ITgComprRepository
{
    Task<TgComprehension> AddTg(Guid appid, long telegramId);
    
}