// <copyright file="TgBotControllerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DTO.DTO.Friend;
using Domain.DTO.DTO.Pozdr;
using Domain.Interfaces.IServices;
using Domain.Models;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProxyAPILevel;
using Xunit;

public class TgBotControllerTests
{
    private readonly Mock<ITgBotService> mockService;
    private readonly TgBotController controller;

    public TgBotControllerTests()
    {
        this.mockService = new Mock<ITgBotService>();
        this.controller = new TgBotController(this.mockService.Object);
    }

    [Fact]
    public async Task GetTodayBirthdays_WhenCalled_ReturnsOkWithListOfFriends()
    {
        // Arrange
        var friendList = new FriendList
        {
            Appid = Guid.NewGuid(),
            FriendUsername = "friend123",
            FriendName = "Bob",
            DateBirth = DateOnly.FromDateTime(DateTime.Now), // День рождения сегодня
            IdPozdr = null, // Если IdPozdr не нужно, установите в null
            App = new User { Appid = Guid.NewGuid() }, // Убедитесь, что объект User также инициализирован
            IdPozdrNavigation = null, // Если IdPozdrNavigation не нужен, установите в null
        };

        var friends = new List<FriendList> { friendList }; // Добавляем друга в список
        this.mockService.Setup(s => s.SelectForTodayBithrdayAsync()).ReturnsAsync(friends);

        // Act
        var res = await this.controller.GetTodayBirthdays();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(res);
        var returnedTodayBirthdays = Assert.IsType<List<FriendList>>(okResult.Value);
        Assert.Single(returnedTodayBirthdays);
        Assert.Equal(friendList.FriendUsername, returnedTodayBirthdays.First().FriendUsername);
    }

    [Fact]
    public async Task GetTgId_WhenTgIdExists_ReturnsOk()
    {
        // Arrange
        var appId = Guid.NewGuid();
        var tgIdDto = new TgIdDto { TgId = 123456789 };
        this.mockService.Setup(s => s.GetTgIdAsync(It.IsAny<AppIdDto>())).ReturnsAsync(tgIdDto);

        // Act
        var result = await this.controller.GetTgId(appId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedTgId = Assert.IsType<TgIdDto>(okResult.Value);
        Assert.Equal(tgIdDto.TgId, returnedTgId.TgId);
    }

    [Fact]
    public async Task GetTgId_WhenTgIdDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var appId = Guid.NewGuid();
        this.mockService.Setup(s => s.GetTgIdAsync(It.IsAny<AppIdDto>())).ReturnsAsync((TgIdDto)null);

        // Act
        var result = await this.controller.GetTgId(appId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Telegram ID not found", notFoundResult.Value);
    }

    [Fact]
    public async Task GetPozdrikById_WhenPozdrikExists_ReturnsOk()
    {
        // Arrange
        var pozdrikIdDto = new PozdrikIdDto { PozdrikId = 1 };
        var expectedPozdrik = "С днем рождения!";
        this.mockService.Setup(s => s.SelectPozdrStringAsync(pozdrikIdDto)).ReturnsAsync(expectedPozdrik);

        // Act
        var result = await this.controller.GetPozdrikById(pozdrikIdDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var pozdrikString = Assert.IsType<string>(okResult.Value);
        Assert.Equal(expectedPozdrik, pozdrikString);
    }

    [Fact]
    public async Task GetPozdrikById_WhenPozdrikDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var pozdrikIdDto = new PozdrikIdDto { PozdrikId = 1 };
        this.mockService.Setup(s => s.SelectPozdrStringAsync(pozdrikIdDto)).ReturnsAsync((string)null);

        // Act
        var result = await this.controller.GetPozdrikById(pozdrikIdDto);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetPozdrikId_WhenPozdrikIdExists_ReturnsOk()
    {
        // Arrange
        var friend = new FriendDto { AppId = Guid.NewGuid(), FriendName = "Алексей", FriendUsername = "Алексей", DateBirth = DateOnly.FromDateTime(DateTime.Now) };
        var pozdrikIdDto = new PozdrikIdDto { PozdrikId = 1 };
        this.mockService.Setup(s => s.GetPozdrikId(friend)).ReturnsAsync(pozdrikIdDto);

        // Act
        var result = await this.controller.GetPozdrikId(friend);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPozdrikId = Assert.IsType<PozdrikIdDto>(okResult.Value);
        Assert.Equal(pozdrikIdDto.PozdrikId, returnedPozdrikId.PozdrikId);
    }

    [Fact]
    public async Task GetPozdrikId_WhenPozdrikIdDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var friend = new FriendDto { AppId = Guid.NewGuid(), FriendName = "Алексей", FriendUsername = "Алексей", DateBirth = DateOnly.FromDateTime(DateTime.Now) };
        this.mockService.Setup(s => s.GetPozdrikId(friend)).ReturnsAsync((PozdrikIdDto)null);

        // Act
        var result = await this.controller.GetPozdrikId(friend);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
    }
}
