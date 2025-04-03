using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Infrastructure.DATA;
using Domain.Models;
using UseCases.Repositoties;

public class MailComprRepositoryTests
{
    private ApplicationContext GetInMemoryContext()
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

        var result = await repository.AddMailAsync(appId, mail);

        Assert.True(result);
        Assert.Equal(1, await context.MailComprehensions.CountAsync());
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

        await repository.AddMailAsync(appId1, mail); 
        var result = await repository.AddMailAsync(appId2, mail); 

        Assert.False(result);
        Assert.Equal(1, await context.MailComprehensions.CountAsync());
    }
    [Fact]
    public async Task GetIdByMailAsync_ShouldReturnAppId_WhenMailExists()
    {
        using var context = GetInMemoryContext();
        var repository = new MailComprRepository(context);

        var appId = Guid.NewGuid();
        var mail = "findme@example.com";

        await repository.AddMailAsync(appId, mail);
    
        var result = await repository.GetIdByMailAsync(mail);

        Assert.Equal(appId, result);
    }
    [Fact]
    public async Task GetIdByMailAsync_ShouldReturnEmptyGuid_WhenMailDoesNotExist()
    {
        using var context = GetInMemoryContext();
        var repository = new MailComprRepository(context);

        var result = await repository.GetIdByMailAsync("notfound@example.com");

        Assert.Equal(Guid.Empty, result);
    }
    [Fact]
    public async Task GetMailByIdAsync_ShouldReturnMail_WhenAppIdExists()
    {
        using var context = GetInMemoryContext();
        var repository = new MailComprRepository(context);

        var appId = Guid.NewGuid();
        var mail = "lookup@example.com";

        await repository.AddMailAsync(appId, mail);
    
        var result = await repository.GetMailByIdAsync(appId);

        Assert.Equal(mail, result);
    }
    [Fact]
    public async Task GetMailByIdAsync_ShouldReturnNull_WhenAppIdDoesNotExist()
    {
        using var context = GetInMemoryContext();
        var repository = new MailComprRepository(context);

        var result = await repository.GetMailByIdAsync(Guid.NewGuid());

        Assert.Null(result);
    }
    [Fact]
    public async Task ExistByMailAsync_ShouldReturnTrue_WhenMailExists()
    {
        using var context = GetInMemoryContext();
        var repository = new MailComprRepository(context);

        var mail = "exists@example.com";
        await repository.AddMailAsync(Guid.NewGuid(), mail);

        var result = await repository.ExistByMailAsync(mail);

        Assert.True(result);
    }
    [Fact]
    public async Task ExistByMailAsync_ShouldReturnFalse_WhenMailDoesNotExist()
    {
        using var context = GetInMemoryContext();
        var repository = new MailComprRepository(context);

        var result = await repository.ExistByMailAsync("notfound@example.com");

        Assert.False(result);
    }

}