using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace UseCases.Repositoties;

public class FriendRepository: IFriendRepository
{
    private readonly DbContext _context;

    public FriendRepository(DbContext context)
    {
        _context = context;
    }
    public async Task<bool> AddFriendInListAsync(FriendList friendList)
    {
        _context.Set<FriendList>().Add(friendList);
        int affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0; // Возвращаем true, если изменения были сохранены
    }

    public Task<bool> DeleteFriendFromListAsync(FriendList friendList)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateFriendInListAsync(FriendList friendList)
    {
        
    }

    public async Task<List<FriendList>> SelectByAppIdAsync(Guid appid)
    {
        return await _context.Set<FriendList>().Where(f =>f.Appid == appid)
            .ToListAsync();
    }

    public async Task<int?> GetPozdrikIdAsync(string username, Guid appid)
    {
        return await _context.Set<FriendList>()
            .Where(f => f.FriendUsername == username && f.Appid == appid)
            .Select(f => f.IdPozdr)
            .FirstOrDefaultAsync();
    }

    public async Task<List<FriendList>> SelectForTodayBithrdayAsync()
    {
        return await _context.Set<FriendList>()
            .Where(f =>f.DateBirth.DayOfYear == DateTime.Now.DayOfYear)
            .ToListAsync();
    }
}