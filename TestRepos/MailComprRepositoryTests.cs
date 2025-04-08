// <copyright file="MailComprRepositoryTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.DATA;
using Microsoft.EntityFrameworkCore;
using UseCases.Repositoties;
using Xunit;

public class MailComprRepositoryTests
{
    private static ApplicationContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationContext(options);
    }

    [Fact]
    public async Task AddMailAsync_ShouldAddMail_WhenMailDoesNotExist()
    {
        using var context = GetInMemoryContext();
        var repository = new MailComprRepository(context);

        var appId = Guid.NewGuid();
        var mail = "test@example.com";

        var result = await repository.AddMailAsync(appId, mail).ConfigureAwait(false);

        Assert.True(result);
        Assert.Equal(1, await context.MailComprehensions.CountAsync().ConfigureAwait(false));
        Assert.Contains(context.MailComprehensions, x => x.Mail == mail && x.Appid == appId);
    }

    [Fact]
    public async Task AddMailAsync_ShouldReturnFalse_WhenMailAlreadyExists()
    {
        using var context = GetInMemoryContext();
        var repository = new MailComprRepository(context);

        var appId1 = Guid.NewGuid();
        var appId2 = Guid.NewGuid();
        var mail = "duplicate@example.com";

        await repository.AddMailAsync(appId1, mail).ConfigureAwait(false);
        var result = await repository.AddMailAsync(appId2, mail).ConfigureAwait(false);

        Assert.False(result);
        Assert.Equal(1, await context.MailComprehensions.CountAsync().ConfigureAwait(false));
    }

    [Fact]
    public async Task GetIdByMailAsync_ShouldReturnAppId_WhenMailExists()
    {
        using var context = GetInMemoryContext();
        var repository = new MailComprRepository(context);

        var appId = Guid.NewGuid();
        var mail = "findme@example.com";

        await repository.AddMailAsync(appId, mail).ConfigureAwait(false);

        var result = await repository.GetIdByMailAsync(mail).ConfigureAwait(false);

        Assert.Equal(appId, result);
    }

    [Fact]
    public async Task GetIdByMailAsync_ShouldReturnEmptyGuid_WhenMailDoesNotExist()
    {
        using var context = GetInMemoryContext();
        var repository = new MailComprRepository(context);

        var result = await repository.GetIdByMailAsync("notfound@example.com").ConfigureAwait(false);

        Assert.Equal(Guid.Empty, result);
    }

    [Fact]
    public async Task GetMailByIdAsync_ShouldReturnMail_WhenAppIdExists()
    {
        using var context = GetInMemoryContext();
        var repository = new MailComprRepository(context);

        var appId = Guid.NewGuid();
        var mail = "lookup@example.com";

        await repository.AddMailAsync(appId, mail).ConfigureAwait(false);

        var result = await repository.GetMailByIdAsync(appId).ConfigureAwait(false);

        Assert.Equal(mail, result);
    }

    [Fact]
    public async Task GetMailByIdAsync_ShouldReturnNull_WhenAppIdDoesNotExist()
    {
        using var context = GetInMemoryContext();
        var repository = new MailComprRepository(context);

        var result = await repository.GetMailByIdAsync(Guid.NewGuid()).ConfigureAwait(false);

        Assert.Null(result);
    }

    [Fact]
    public async Task ExistByMailAsync_ShouldReturnTrue_WhenMailExists()
    {
        using var context = GetInMemoryContext();
        var repository = new MailComprRepository(context);

        var mail = "exists@example.com";
        await repository.AddMailAsync(Guid.NewGuid(), mail).ConfigureAwait(false);

        var result = await repository.ExistByMailAsync(mail).ConfigureAwait(false);

        Assert.True(result);
    }

    [Fact]
    public async Task ExistByMailAsync_ShouldReturnFalse_WhenMailDoesNotExist()
    {
        using var context = GetInMemoryContext();
        var repository = new MailComprRepository(context);

        var result = await repository.ExistByMailAsync("notfound@example.com").ConfigureAwait(false);

        Assert.False(result);
    }
}
