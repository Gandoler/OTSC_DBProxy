using Domain.Interfaces;
using Domain.Models;
using Entities.Templates;
using Infrastructure.DATA;
using Microsoft.EntityFrameworkCore;

namespace UseCases.Repositoties;

public class FriendRepository: IFriendRepository
{
    private readonly ApplicationContext _context;

    public FriendRepository(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<bool> AddFriendInListAsync(FriendList friendList)
    {
        _context.Set<FriendList>().Add(friendList);
        int affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0; // Возвращаем true, если изменения были сохранены
    }

    public async Task<bool> DeleteFriendFromListAsync(Guid appid, string friendname)
    {
        var friend = await _context.Set<FriendList>().FirstOrDefaultAsync(f => f.Appid == appid && f.FriendName == friendname);
    
        if (friend == null)
            return false; // Друг не найден

        _context.Set<FriendList>().Remove(friend);
        int affectedRows = await _context.SaveChangesAsync();
    
        return affectedRows > 0;
    }

    public async Task<bool> UpdateFriendInListAsync(FriendList friendList)
    {
        _context.Set<FriendList>().Update(friendList);
        int affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0;
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