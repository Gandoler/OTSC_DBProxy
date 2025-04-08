// <copyright file="FriendRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UseCases.Repositoties;

using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DATA;
using Microsoft.EntityFrameworkCore;

public class FriendRepository : IFriendRepository
{
    private readonly ApplicationContext context;

    public FriendRepository(ApplicationContext context)
    {
        this.context = context;
    }

    // протестил
    public async Task<bool> AddFriendInListAsync(FriendList friendList)
    {
        var userExists = await this.context.Set<FriendList>().Where(f => f.FriendUsername == friendList.FriendUsername && f.Appid == friendList.Appid).AnyAsync().ConfigureAwait(false);
        if (userExists)
        {
            Console.WriteLine($"Firend with Appid {friendList.Appid} and username {friendList.FriendName} already exist.");
            return false;
        }

        this.context.Set<FriendList>().Add(friendList);
        int affectedRows = await this.context.SaveChangesAsync().ConfigureAwait(false);
        return affectedRows > 0;
    }

    // протестил
    public async Task<bool> DeleteFriendFromListAsync(Guid appid, string friendUsername)
    {
        var friend = await this.context.Set<FriendList>().FirstOrDefaultAsync(f => f.Appid == appid && f.FriendUsername == friendUsername).ConfigureAwait(false);

        if (friend == null)
        {
            return false; // Друг не найден
        }

        this.context.Set<FriendList>().Remove(friend);
        int affectedRows = await this.context.SaveChangesAsync().ConfigureAwait(false);

        return affectedRows > 0;
    }

    // протестил
    public async Task<bool> UpdateFriendInListAsync(FriendList friendList)
    {
        this.context.Set<FriendList>().Update(friendList);
        int affectedRows = await this.context.SaveChangesAsync().ConfigureAwait(false);
        return affectedRows > 0;
    }

    // протестил
    public async Task<List<FriendList>> SelectByAppIdAsync(Guid appid)
    {
        return await this.context.Set<FriendList>().Where(f => f.Appid == appid)
            .ToListAsync().ConfigureAwait(false);
    }

    public async Task<int?> GetPozdrikIdAsync(string? username, Guid appid)
    {
        return await this.context.Set<FriendList>()
            .Where(f => f.FriendUsername == username && f.Appid == appid)
            .Select(f => f.IdPozdr)
            .FirstOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task<List<FriendList>> SelectForTodayBithrdayAsync()
    {
        return await this.context.Set<FriendList>()
            .Where(f => f.DateBirth.DayOfYear == DateTime.Now.DayOfYear)
            .ToListAsync().ConfigureAwait(false);
    }

    public async Task<string?> GetFrienNameByPozdrikId(int? pozdrikId)
    {
        return await this.context.Set<FriendList>()
            .Where(f => f.IdPozdr == pozdrikId)
            .Select(f => f.FriendName)
            .FirstOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task<string?> GetFrienUserNameByPozdrikId(int? pozdrikId)
    {
        return await this.context.Set<FriendList>()
            .Where(f => f.IdPozdr == pozdrikId)
            .Select(f => f.FriendUsername)
            .FirstOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task<bool> AddPozdrikIdToFriendAsync(int pozdrikId, string friendUsername, Guid appId)
    {
        FriendList? friend = await this.context.Set<FriendList>()
            .FirstOrDefaultAsync(f => f.FriendUsername == friendUsername && f.Appid == appId).ConfigureAwait(false);
        if (friend != null)
        {
            friend.IdPozdr = pozdrikId;
            this.context.Set<FriendList>().Update(friend);
            int affectedRows = await this.context.SaveChangesAsync().ConfigureAwait(false);
            return affectedRows > 0;
        }

        return false;
    }
}
