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
    public Task<int> AddFriendInListAsync(FriendList friendList)
    {
        _context.Set<FriendList>().Add(friendList);
        await _context.SaveChangesAsync();
        return await _context.
    }

    public Task<int> DeleteFriendFromListAsync(FriendList friendList)
    {
        throw new NotImplementedException();
    }

    public async Task<int> UpdateFriendInListAsync(FriendList friendList)
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