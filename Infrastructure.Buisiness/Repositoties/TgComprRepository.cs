using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace UseCases.Repositoties;

public class TgComprRepository : ITgComprRepository
{
    
    private readonly DbContext _context;

    public TgComprRepository(DbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> AddTgAsync(Guid appid, long telegramId)
    {
        TgComprehension zapiszTg = new TgComprehension {Appid = appid,TgId = telegramId};
        _context.Set<TgComprehension>().Add(zapiszTg);
        int result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Guid> GetIdByTgAsync(long tgId)
    {
        return await _context.Set<TgComprehension>()
            .Where(f => f.TgId == tgId)
            .Select(f => f.Appid)
            .FirstOrDefaultAsync();
    }

    public async Task<long?> GetTgId(Guid appId)
    {
        return await _context.Set<TgComprehension>()
            .Where(f => f.Appid == appId)
            .Select(f => f.TgId)
            .FirstOrDefaultAsync();
    }
}