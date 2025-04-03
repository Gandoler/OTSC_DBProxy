using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Infrastructure.DATA;
using Domain.Models;
using UseCases;

public class PozdrikRepositoryTests
{
    private ApplicationContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;

        return new ApplicationContext(options);
    }
    [Fact]
    public async Task AddIntAndPozhAsync_ShouldAddNewPozdrik_WhenIdDoesNotExist()
    {
        using var context = GetInMemoryContext();
        var repository = new PozdrikRepository(context);

        var result = await repository.AddIntAndPozhAsync(1, "Reading", "Happy Birthday!");

        Assert.True(result);
        var pozdrik = await context.Pozdriks.FindAsync(1);
        Assert.NotNull(pozdrik);
        Assert.Equal("Reading", pozdrik.Interest);
        Assert.Equal("Happy Birthday!", pozdrik.Pozhelanie);
    }
    [Fact]
    public async Task AddIntAndPozhAsync_ShouldUpdateExistingPozdrik_WhenIdExists()
    {
        using var context = GetInMemoryContext();
        var repository = new PozdrikRepository(context);

        await repository.AddIntAndPozhAsync(1, "Music", "Best Wishes!");
        var result = await repository.AddIntAndPozhAsync(2, "Books", "Congrats!");

        Assert.True(result);
        var pozdrik = await context.Pozdriks.FindAsync(2);
        Assert.Equal("Books", pozdrik.Interest);
        Assert.Equal("Congrats!", pozdrik.Pozhelanie);
    }
    [Fact]
    public async Task CreatePozdrikAsync_ShouldCreatePozdrik_AndReturnId()
    {
        using var context = GetInMemoryContext();
        var repository = new PozdrikRepository(context);

        var id = await repository.CreatePozdrikAsync("Sports", "Congratulations!");

        var pozdrik = await context.Pozdriks.FindAsync(id);
        Assert.NotNull(pozdrik);
        Assert.Equal("Sports", pozdrik.Interest);
        Assert.Equal("Congratulations!", pozdrik.Pozhelanie);
    }
    [Fact]
    public async Task SelectIntAndPozhAsync_ShouldReturnInterestAndPozhelanie_WhenIdExists()
    {
        using var context = GetInMemoryContext();
        var repository = new PozdrikRepository(context);

        await repository.AddIntAndPozhAsync(1, "Travel", "Good Luck!");

        var (interest, pozhelanie) = await repository.SelectIntAndPozhAsync(1);

        Assert.Equal("Travel", interest);
        Assert.Equal("Good Luck!", pozhelanie);
    }
    [Fact]
    public async Task SelectPozdrikAsync_ShouldReturnTextpozdr_WhenIdExists()
    {
        using var context = GetInMemoryContext();
        var repository = new PozdrikRepository(context);
        context.Set<Pozdrik>().Add(new Pozdrik{IdPozdr = 1,Interest = "Reading",Pozhelanie = "Happy Birthday!"});
        await context.SaveChangesAsync();
        await repository.AddPozdrAsync(1, "Happy Anniversary!");

        var result = await repository.SelectPozdrikAsync(1);

        Assert.Equal("Happy Anniversary!", result);
    }
    [Fact]
    public async Task AddPozdrAsync_ShouldUpdatePozdrik_WhenIdExists()
    {
        using var context = GetInMemoryContext();
        var repository = new PozdrikRepository(context);

        await repository.AddIntAndPozhAsync(1, "Photography", "Best wishes!");
        var result = await repository.AddPozdrAsync(1, "Merry Christmas!");

        Assert.True(result);
        var pozdrik = await context.Pozdriks.FindAsync(1);
        Assert.Equal("Merry Christmas!", pozdrik.Textpozdr);
    }
    [Fact]
    public async Task AddPozdrAsync_ShouldReturnFalse_WhenIdDoesNotExist()
    {
        using var context = GetInMemoryContext();
        var repository = new PozdrikRepository(context);

        var result = await repository.AddPozdrAsync(99, "Happy New Year!");

        Assert.False(result);
    }
    [Fact]
    public async Task SelectEmptyOrNullPozdrstringAsync_ShouldReturnIdsWithEmptyTextpozdr()
    {
        using var context = GetInMemoryContext();
        var repository = new PozdrikRepository(context);

        await repository.AddIntAndPozhAsync(1, "Cooking", "Enjoy!");
        await repository.AddPozdrAsync(1, "");
        await repository.AddIntAndPozhAsync(2, "Movies", "Have fun!");
        await repository.AddPozdrAsync(2, null);
        await repository.AddIntAndPozhAsync(3, "Books", "Happy Reading!");
        await repository.AddPozdrAsync(3, "Good vibes only!");

        var result = await repository.SelectEmptyOrNullPozdrstringAsync();

        Assert.Contains(1, result);
        Assert.Contains(2, result);
        Assert.DoesNotContain(3, result);
    }

}