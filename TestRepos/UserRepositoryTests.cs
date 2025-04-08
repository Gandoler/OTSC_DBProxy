// <copyright file="UserRepositoryTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.DATA;
using Microsoft.EntityFrameworkCore;
using UseCases.Repositoties;
using Xunit;

public class UserRepositoryTests
{
    private static ApplicationContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationContext(options);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnTrue_WhenUserDoesNotExist()
    {
        using var context = GetInMemoryContext();
        var repository = new UserRepository(context);
        var user = new User { Appid = Guid.NewGuid(), Login = "test_user", Password = "password" };

        bool result = await repository.CreateAsync(user).ConfigureAwait(false);

        Assert.True(result);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnFalse_WhenUserAlreadyExists()
    {
        using var context = GetInMemoryContext();
        var repository = new UserRepository(context);
        var user = new User { Appid = Guid.NewGuid(), Login = "test_user", Password = "password" };
        await repository.CreateAsync(user).ConfigureAwait(false);

        bool result = await repository.CreateAsync(user).ConfigureAwait(false);

        Assert.False(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnTrue_WhenUserExists()
    {
        using var context = GetInMemoryContext();
        var repository = new UserRepository(context);
        var user = new User { Appid = Guid.NewGuid(), Login = "test_user", Password = "old_password" };
        await repository.CreateAsync(user).ConfigureAwait(false);

        user.Password = "new_password";
        bool result = await repository.UpdateAsync(user).ConfigureAwait(false);

        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnFalse_WhenUserDoesNotExist()
    {
        using var context = GetInMemoryContext();
        var repository = new UserRepository(context);
        var user = new User { Appid = Guid.NewGuid(), Login = "non_existent", Password = "password" };

        bool result = await repository.UpdateAsync(user).ConfigureAwait(false);

        Assert.False(result);
    }

    [Fact]
    public async Task ForPswAndLoginCheckAsync_ShouldReturnTrue_WhenCredentialsAreCorrect()
    {
        using var context = GetInMemoryContext();
        var repository = new UserRepository(context);
        var user = new User { Appid = Guid.NewGuid(), Login = "test_user", Password = "password" };
        await repository.CreateAsync(user).ConfigureAwait(false);

        bool result = await repository.ForPswAndLoginCheckAsync(user).ConfigureAwait(false);

        Assert.True(result);
    }

    [Fact]
    public async Task ForPswAndLoginCheckAsync_ShouldReturnFalse_WhenCredentialsAreIncorrect()
    {
        using var context = GetInMemoryContext();
        var repository = new UserRepository(context);
        var user = new User { Appid = Guid.NewGuid(), Login = "test_user", Password = "password" };
        await repository.CreateAsync(user).ConfigureAwait(false);

        var wrongUser = new User { Login = "test_user", Password = "wrong_password" };
        bool result = await repository.ForPswAndLoginCheckAsync(wrongUser).ConfigureAwait(false);

        Assert.False(result);
    }

    [Fact]
    public async Task ExicstCheckByLoginAsync_ShouldReturnTrue_WhenUserExists()
    {
        using var context = GetInMemoryContext();
        var repository = new UserRepository(context);
        var user = new User { Appid = Guid.NewGuid(), Login = "test_user", Password = "password" };
        await repository.CreateAsync(user).ConfigureAwait(false);

        bool result = await repository.ExicstCheckByLoginAsync("test_user").ConfigureAwait(false);

        Assert.True(result);
    }

    [Fact]
    public async Task ExicstCheckByLoginAsync_ShouldReturnFalse_WhenUserDoesNotExist()
    {
        using var context = GetInMemoryContext();
        var repository = new UserRepository(context);

        bool result = await repository.ExicstCheckByLoginAsync("non_existent").ConfigureAwait(false);

        Assert.False(result);
    }

    [Fact]
    public async Task GetUserByLoginAsync_ShouldReturnUserId_WhenUserExists()
    {
        using var context = GetInMemoryContext();
        var repository = new UserRepository(context);
        var user = new User { Appid = Guid.NewGuid(), Login = "test_user", Password = "password" };
        await repository.CreateAsync(user).ConfigureAwait(false);

        Guid? result = await repository.GetUserByLoginAsync("test_user").ConfigureAwait(false);

        Assert.Equal(user.Appid, result);
    }

    [Fact]
    public async Task GetUserByLoginAsync_ShouldReturnNull_WhenUserDoesNotExist()
    {
        using var context = GetInMemoryContext();
        var repository = new UserRepository(context);

        Guid? result = await repository.GetUserByLoginAsync("non_existent").ConfigureAwait(false);

        Assert.Equal(Guid.Empty, result); // Проверяем, что вернулся Guid.Empty
    }

    [Fact]
    public async Task GetUserByIdAsync_ShouldReturnLogin_WhenUserExists()
    {
        using var context = GetInMemoryContext();
        var repository = new UserRepository(context);
        var user = new User { Appid = Guid.NewGuid(), Login = "test_user", Password = "password" };
        await repository.CreateAsync(user).ConfigureAwait(false);

        string? result = await repository.GetUserByIdAsync(user.Appid).ConfigureAwait(false);

        Assert.Equal("test_user", result);
    }

    [Fact]
    public async Task GetUserByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
    {
        using var context = GetInMemoryContext();
        var repository = new UserRepository(context);

        string? result = await repository.GetUserByIdAsync(Guid.NewGuid()).ConfigureAwait(false);

        Assert.Null(result);
    }
}
