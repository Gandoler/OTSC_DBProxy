// <copyright file="FriendRepositoryTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TestRepos;

using Domain.Models;
using Infrastructure.DATA;
using Microsoft.EntityFrameworkCore;
using UseCases.Repositoties;
using Xunit;

public class FriendRepositoryTests
{
    private static ApplicationContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationContext(options);
    }

    [Fact]
    public async Task AddFriendInListAsync_ShouldAddFriend()
    {
        using var context = GetInMemoryContext();
        var repository = new FriendRepository(context);

        var friend = new FriendList
        {
            Appid = Guid.NewGuid(),
            FriendUsername = "test_user",
            FriendName = "Test User",
            DateBirth = new DateOnly(2000, 1, 1),
            IdPozdr = null,
        };

        var result = await repository.AddFriendInListAsync(friend).ConfigureAwait(false);

        Assert.True(result);
        Assert.Single(await context.Set<FriendList>().ToListAsync().ConfigureAwait(false));
    }

    [Fact]
    public async Task DeleteFriendFromListAsync_ShouldRemoveFriend()
    {
        using var context = GetInMemoryContext();
        var repository = new FriendRepository(context);

        var friend = new FriendList
        {
            Appid = Guid.NewGuid(),
            FriendUsername = "test_user",
            FriendName = "Test User",
            DateBirth = new DateOnly(2000, 1, 1),
            IdPozdr = null,
        };

        context.Set<FriendList>().Add(friend);
        await context.SaveChangesAsync().ConfigureAwait(false);

        var result = await repository.DeleteFriendFromListAsync(friend.Appid, friend.FriendUsername).ConfigureAwait(false);

        Assert.True(result);
        Assert.Empty(await context.Set<FriendList>().ToListAsync().ConfigureAwait(false));
    }

    [Fact]
    public async Task UpdateFriendInListAsync_ShouldUpdateFriend()
    {
        using var context = GetInMemoryContext();
        var repository = new FriendRepository(context);

        var friend = new FriendList
        {
            Appid = Guid.NewGuid(),
            FriendUsername = "test_user",
            FriendName = "Old Name",
            DateBirth = new DateOnly(2000, 1, 1),
            IdPozdr = null,
        };

        context.Set<FriendList>().Add(friend);
        await context.SaveChangesAsync().ConfigureAwait(false);

        friend.FriendName = "New Name";
        var result = await repository.UpdateFriendInListAsync(friend).ConfigureAwait(false);

        var updatedFriend = await context.Set<FriendList>().FirstOrDefaultAsync(f => f.FriendUsername == "test_user").ConfigureAwait(false);

        Assert.True(result);
        Assert.NotNull(updatedFriend);
        Assert.Equal("New Name", updatedFriend.FriendName);
    }

    [Fact]
    public async Task SelectByAppIdAsync_ShouldReturnCorrectFriends()
    {
        using var context = GetInMemoryContext();
        var repository = new FriendRepository(context);

        var appId = Guid.NewGuid();

        var friend1 = new FriendList { Appid = appId, FriendUsername = "user1", FriendName = "User One", DateBirth = new DateOnly(2000, 1, 1) };
        var friend2 = new FriendList { Appid = appId, FriendUsername = "user2", FriendName = "User Two", DateBirth = new DateOnly(2000, 2, 2) };
        var friend3 = new FriendList { Appid = Guid.NewGuid(), FriendUsername = "user3", FriendName = "User Three", DateBirth = new DateOnly(2000, 3, 3) };

        context.Set<FriendList>().AddRange(friend1, friend2, friend3);
        await context.SaveChangesAsync().ConfigureAwait(false);

        var friends = await repository.SelectByAppIdAsync(appId).ConfigureAwait(false);

        Assert.Equal(2, friends.Count);
        Assert.Contains(friends, f => f.FriendUsername == "user1");
        Assert.Contains(friends, f => f.FriendUsername == "user2");
        Assert.NotEmpty(friends);
    }

    [Fact]
    public async Task GetPozdrikIdAsync_ShouldReturnPozdrikId()
    {
        using var context = GetInMemoryContext();
        var repository = new FriendRepository(context);

        var appId = Guid.NewGuid();
        var friend = new FriendList
        {
            Appid = appId,
            FriendUsername = "test_user",
            FriendName = "Test User",
            DateBirth = new DateOnly(2000, 1, 1),
            IdPozdr = 123, // Значение IdPozdr
        };

        context.Set<FriendList>().Add(friend);
        await context.SaveChangesAsync().ConfigureAwait(false);

        var pozdrikId = await repository.GetPozdrikIdAsync("test_user", appId).ConfigureAwait(false);

        Assert.Equal(123, pozdrikId);
    }

    [Fact]
    public async Task SelectForTodayBithrdayAsync_ShouldReturnFriendsWithBirthdayToday()
    {
        using var context = GetInMemoryContext();
        var repository = new FriendRepository(context);

        var appId = Guid.NewGuid();
        var today = DateOnly.FromDateTime(DateTime.Now);

        var friend1 = new FriendList { Appid = appId, FriendUsername = "user1", FriendName = "User One", DateBirth = today };
        var friend2 = new FriendList { Appid = appId, FriendUsername = "user2", FriendName = "User Two", DateBirth = today };
        var friend3 = new FriendList { Appid = appId, FriendUsername = "user3", FriendName = "User Three", DateBirth = new DateOnly(1999, 12, 25) };

        context.Set<FriendList>().AddRange(friend1, friend2, friend3);
        await context.SaveChangesAsync().ConfigureAwait(false);

        var friends = await repository.SelectForTodayBithrdayAsync().ConfigureAwait(false);

        Assert.Equal(2, friends.Count); // Только два друга должны иметь день рождения сегодня
        Assert.Contains(friends, f => f.FriendUsername == "user1");
        Assert.Contains(friends, f => f.FriendUsername == "user2");
        Assert.DoesNotContain(friends, f => f.FriendUsername == "user3");
    }

    [Fact]
    public async Task GetFrienNameByPozdrikId_ShouldReturnFriendName()
    {
        using var context = GetInMemoryContext();
        var repository = new FriendRepository(context);

        var friend = new FriendList
        {
            Appid = Guid.NewGuid(),
            FriendUsername = "test_user",
            FriendName = "Test User",
            DateBirth = new DateOnly(2000, 1, 1),
            IdPozdr = 123,
        };

        context.Set<FriendList>().Add(friend);
        await context.SaveChangesAsync().ConfigureAwait(false);

        var friendName = await repository.GetFrienNameByPozdrikId(123).ConfigureAwait(false);

        Assert.Equal("Test User", friendName);
    }

    [Fact]
    public async Task GetFrienUserNameByPozdrikId_ShouldReturnFriendUsername()
    {
        using var context = GetInMemoryContext();
        var repository = new FriendRepository(context);

        var friend = new FriendList
        {
            Appid = Guid.NewGuid(),
            FriendUsername = "test_user",
            FriendName = "Test User",
            DateBirth = new DateOnly(2000, 1, 1),
            IdPozdr = 123,
        };

        context.Set<FriendList>().Add(friend);
        await context.SaveChangesAsync().ConfigureAwait(false);

        var friendUsername = await repository.GetFrienUserNameByPozdrikId(123).ConfigureAwait(false);

        Assert.Equal("test_user", friendUsername);
    }

    [Fact]
    public async Task AddPozdrikIdToFriendAsync_ShouldAddPozdrikId()
    {
        using var context = GetInMemoryContext();
        var repository = new FriendRepository(context);

        var appId = Guid.NewGuid();
        var friend = new FriendList
        {
            Appid = appId,
            FriendUsername = "test_user",
            FriendName = "Test User",
            DateBirth = new DateOnly(2000, 1, 1),
            IdPozdr = null,
        };

        context.Set<FriendList>().Add(friend);
        await context.SaveChangesAsync().ConfigureAwait(false);

        var result = await repository.AddPozdrikIdToFriendAsync(123, "test_user", appId).ConfigureAwait(false);

        var updatedFriend = await context.Set<FriendList>().FirstOrDefaultAsync(f => f.FriendUsername == "test_user").ConfigureAwait(false);

        Assert.True(result);
        Assert.NotNull(updatedFriend);
        Assert.Equal(123, updatedFriend.IdPozdr);
    }
}
