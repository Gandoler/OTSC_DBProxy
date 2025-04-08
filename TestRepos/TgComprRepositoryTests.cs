// <copyright file="TgComprRepositoryTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.DATA;
using Microsoft.EntityFrameworkCore;
using UseCases.Repositoties;
using Xunit;

public class TgComprRepositoryTests
{
    private static ApplicationContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Создаём уникальную БД в памяти
            .Options;
        return new ApplicationContext(options);
    }

    [Fact]
    public async Task AddTgAsync_ShouldAddTelegramId_WhenItDoesNotExist()
    {
        using var context = GetInMemoryContext();
        var repository = new TgComprRepository(context);

        Guid appId = Guid.NewGuid();
        long telegramId = 123456789;

        bool result = await repository.AddTgAsync(appId, telegramId).ConfigureAwait(false);

        Assert.True(result); // Проверяем, что добавление прошло успешно
        Assert.NotNull(await context.Set<TgComprehension>().FirstOrDefaultAsync(x => x.TgId == telegramId).ConfigureAwait(false));
    }

    [Fact]
    public async Task AddTgAsync_ShouldReturnFalse_WhenTelegramIdAlreadyExists()
    {
        using var context = GetInMemoryContext();
        var repository = new TgComprRepository(context);

        Guid appId = Guid.NewGuid();
        long telegramId = 123456789;

        // Добавляем первый раз
        await repository.AddTgAsync(appId, telegramId).ConfigureAwait(false);

        // Попытка добавить тот же Telegram ID снова
        bool result = await repository.AddTgAsync(Guid.NewGuid(), telegramId).ConfigureAwait(false);

        Assert.False(result); // Должен вернуть false, так как ID уже есть
    }

    [Fact]
    public async Task GetIdByTgAsync_ShouldReturnAppid_WhenTelegramIdExists()
    {
        using var context = GetInMemoryContext();
        var repository = new TgComprRepository(context);

        Guid expectedAppId = Guid.NewGuid();
        long telegramId = 123456789;

        await repository.AddTgAsync(expectedAppId, telegramId).ConfigureAwait(false);

        Guid result = await repository.GetIdByTgAsync(telegramId).ConfigureAwait(false);

        Assert.Equal(expectedAppId, result);
    }

    [Fact]
    public async Task GetIdByTgAsync_ShouldReturnEmptyGuid_WhenTelegramIdDoesNotExist()
    {
        using var context = GetInMemoryContext();
        var repository = new TgComprRepository(context);

        Guid result = await repository.GetIdByTgAsync(999999999).ConfigureAwait(false); // Несуществующий ID

        Assert.Equal(Guid.Empty, result); // Должен вернуть пустой GUID
    }

    [Fact]
    public async Task GetTgId_ShouldReturnTelegramId_WhenAppIdExists()
    {
        using var context = GetInMemoryContext();
        var repository = new TgComprRepository(context);

        Guid appId = Guid.NewGuid();
        long expectedTelegramId = 123456789;

        await repository.AddTgAsync(appId, expectedTelegramId).ConfigureAwait(false);

        long? result = await repository.GetTgId(appId).ConfigureAwait(false);

        Assert.Equal(expectedTelegramId, result);
    }

    [Fact]
    public async Task GetTgId_ShouldReturnNull_WhenAppIdDoesNotExist()
    {
        using var context = GetInMemoryContext();
        var repository = new TgComprRepository(context);
        Guid id = Guid.NewGuid();
        context.Set<TgComprehension>().Add(new TgComprehension { Appid = id, TgId = 123456789 });
        await context.SaveChangesAsync().ConfigureAwait(false);
        long? result = await repository.GetTgId(Guid.NewGuid()).ConfigureAwait(false);

        Assert.Null(result); // Должен вернуть null
    }
}
