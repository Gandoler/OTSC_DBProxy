// <copyright file="PozdrikRepositoryTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.DATA;
using Microsoft.EntityFrameworkCore;
using UseCases;
using Xunit;

public class PozdrikRepositoryTests
{
    private static ApplicationContext GetInMemoryContext()
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

        var result = await repository.AddIntAndPozhAsync(1, "Reading", "Happy Birthday!").ConfigureAwait(false);

        Assert.True(result);
        var pozdrik = await context.Pozdriks.FindAsync(1).ConfigureAwait(false);
        Assert.NotNull(pozdrik);
        Assert.Equal("Reading", pozdrik.Interest);
        Assert.Equal("Happy Birthday!", pozdrik.Pozhelanie);
    }

    [Fact]
    public async Task AddIntAndPozhAsync_ShouldUpdateExistingPozdrik_WhenIdExists()
    {
        using var context = GetInMemoryContext();
        var repository = new PozdrikRepository(context);

        await repository.AddIntAndPozhAsync(1, "Music", "Best Wishes!").ConfigureAwait(false);
        var result = await repository.AddIntAndPozhAsync(2, "Books", "Congrats!").ConfigureAwait(false);

        Assert.True(result);
        var pozdrik = await context.Pozdriks.FindAsync(2).ConfigureAwait(false);
        Assert.Equal("Books", pozdrik.Interest);
        Assert.Equal("Congrats!", pozdrik.Pozhelanie);
    }

    [Fact]
    public async Task CreatePozdrikAsync_ShouldCreatePozdrik_AndReturnId()
    {
        using var context = GetInMemoryContext();
        var repository = new PozdrikRepository(context);

        var id = await repository.CreatePozdrikAsync("Sports", "Congratulations!").ConfigureAwait(false);

        var pozdrik = await context.Pozdriks.FindAsync(id).ConfigureAwait(false);
        Assert.NotNull(pozdrik);
        Assert.Equal("Sports", pozdrik.Interest);
        Assert.Equal("Congratulations!", pozdrik.Pozhelanie);
    }

    [Fact]
    public async Task SelectIntAndPozhAsync_ShouldReturnInterestAndPozhelanie_WhenIdExists()
    {
        using var context = GetInMemoryContext();
        var repository = new PozdrikRepository(context);

        await repository.AddIntAndPozhAsync(1, "Travel", "Good Luck!").ConfigureAwait(false);

        var (interest, pozhelanie) = await repository.SelectIntAndPozhAsync(1).ConfigureAwait(false);

        Assert.Equal("Travel", interest);
        Assert.Equal("Good Luck!", pozhelanie);
    }

    [Fact]
    public async Task SelectPozdrikAsync_ShouldReturnTextpozdr_WhenIdExists()
    {
        using var context = GetInMemoryContext();
        var repository = new PozdrikRepository(context);
        context.Set<Pozdrik>().Add(new Pozdrik { IdPozdr = 1, Interest = "Reading", Pozhelanie = "Happy Birthday!" });
        await context.SaveChangesAsync().ConfigureAwait(false);
        await repository.AddPozdrAsync(1, "Happy Anniversary!").ConfigureAwait(false);

        var result = await repository.SelectPozdrikAsync(1).ConfigureAwait(false);

        Assert.Equal("Happy Anniversary!", result);
    }

    [Fact]
    public async Task AddPozdrAsync_ShouldUpdatePozdrik_WhenIdExists()
    {
        using var context = GetInMemoryContext();
        var repository = new PozdrikRepository(context);

        await repository.AddIntAndPozhAsync(1, "Photography", "Best wishes!").ConfigureAwait(false);
        var result = await repository.AddPozdrAsync(1, "Merry Christmas!").ConfigureAwait(false);

        Assert.True(result);
        var pozdrik = await context.Pozdriks.FindAsync(1).ConfigureAwait(false);
        Assert.Equal("Merry Christmas!", pozdrik.Textpozdr);
    }

    [Fact]
    public async Task AddPozdrAsync_ShouldReturnFalse_WhenIdDoesNotExist()
    {
        using var context = GetInMemoryContext();
        var repository = new PozdrikRepository(context);

        var result = await repository.AddPozdrAsync(99, "Happy New Year!").ConfigureAwait(false);

        Assert.False(result);
    }

    [Fact]
    public async Task SelectEmptyOrNullPozdrstringAsync_ShouldReturnIdsWithEmptyTextpozdr()
    {
        using var context = GetInMemoryContext();
        var repository = new PozdrikRepository(context);

        await repository.AddIntAndPozhAsync(1, "Cooking", "Enjoy!").ConfigureAwait(false);
        await repository.AddPozdrAsync(1, string.Empty).ConfigureAwait(false);
        await repository.AddIntAndPozhAsync(2, "Movies", "Have fun!").ConfigureAwait(false);
        await repository.AddPozdrAsync(2, null).ConfigureAwait(false);
        await repository.AddIntAndPozhAsync(3, "Books", "Happy Reading!").ConfigureAwait(false);
        await repository.AddPozdrAsync(3, "Good vibes only!").ConfigureAwait(false);

        var result = await repository.SelectEmptyOrNullPozdrstringAsync().ConfigureAwait(false);

        Assert.Contains(1, result);
        Assert.Contains(2, result);
        Assert.DoesNotContain(3, result);
    }
}
