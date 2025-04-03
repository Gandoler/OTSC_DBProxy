using Domain.Interfaces;
using Domain.Models;
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
    //протестил
    public async Task<bool> AddFriendInListAsync(FriendList friendList)
    {
        var userExists = await _context.Set<FriendList>().Where(f =>f.FriendUsername==friendList.FriendUsername && f.Appid == friendList.Appid).AnyAsync();
        if (userExists)
        {
            Console.WriteLine($"Firend with Appid {friendList.Appid} and username {friendList.FriendName} already exist.");
           return false;
        }

        _context.Set<FriendList>().Add(friendList);
        int affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0;
    }
    //протестил
    public async Task<bool> DeleteFriendFromListAsync(Guid appid, string friendUsername)
    {
        var friend = await _context.Set<FriendList>().FirstOrDefaultAsync(f => f.Appid == appid && f.FriendUsername == friendUsername);
    
        if (friend == null)
            return false; // Друг не найден

        _context.Set<FriendList>().Remove(friend);
        int affectedRows = await _context.SaveChangesAsync();
    
        return affectedRows > 0;
    }
    //протестил
    public async Task<bool> UpdateFriendInListAsync(FriendList friendList)
    {
        _context.Set<FriendList>().Update(friendList);
        int affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0;
    }
    //протестил
    public async Task<List<FriendList>> SelectByAppIdAsync(Guid appid)
    {
        return await _context.Set<FriendList>().Where(f =>f.Appid == appid)
            .ToListAsync();
    }

    public async Task<int?> GetPozdrikIdAsync(string? username, Guid appid)
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

    public async Task<string?> GetFrienNameByPozdrikId(int? pozdrikId)
    {
        return await _context.Set<FriendList>()
            .Where(f => f.IdPozdr == pozdrikId)
            .Select(f => f.FriendName)
            .FirstOrDefaultAsync();
    }

    public async Task<string?> GetFrienUserNameByPozdrikId(int? pozdrikId)
    {
        return await _context.Set<FriendList>()
            .Where(f => f.IdPozdr == pozdrikId)
            .Select(f => f.FriendUsername)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> AddPozdrikIdToFriendAsync(int pozdrikId, string friendUsername, Guid appId)
    {
       FriendList? friend =await _context.Set<FriendList>()
           .FirstOrDefaultAsync(f => f.FriendUsername == friendUsername && f.Appid == appId);
       if (friend != null)
       {
           friend.IdPozdr = pozdrikId;
           _context.Set<FriendList>().Update(friend);
           int affectedRows = await _context.SaveChangesAsync();
           return affectedRows > 0;
       }
       return false;
    }
}