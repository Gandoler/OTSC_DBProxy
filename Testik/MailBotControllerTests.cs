// <copyright file="MailBotControllerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
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

public class MailBotControllerTests
{
    private readonly Mock<IMailBotService> mockService;
    private readonly MailBotController controller;

    public MailBotControllerTests()
    {
        this.mockService = new Mock<IMailBotService>();
        this.controller = new MailBotController(this.mockService.Object);
    }

    [Fact]
    public async Task GetPozdrikIdForMail_ShouldReturnOk_WhenPozdrikIdFound()
    {
        // Arrange
        var friendDto = new FriendDto
        {
            AppId = Guid.NewGuid(),
            FriendUsername = "friend123",
            FriendName = "Alice",
            DateBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-25)),
        };

        var pozdrikIdDto = new PozdrikIdDto { PozdrikId = 100 };
        this.mockService.Setup(s => s.GetPozdrikId(friendDto)).ReturnsAsync(pozdrikIdDto);

        // Act
        var result = await this.controller.GetPozdrikIdForMail(friendDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPozdrikId = Assert.IsType<PozdrikIdDto>(okResult.Value);
        Assert.Equal(pozdrikIdDto.PozdrikId, returnedPozdrikId.PozdrikId);
    }

    [Fact]
    public async Task GetPozdrikIdForMail_ShouldReturnNotFound_WhenPozdrikIdNotFound()
    {
        // Arrange
        var friendDto = new FriendDto
        {
            AppId = Guid.NewGuid(),
            FriendUsername = "friend123",
            FriendName = "Bob",
            DateBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)), // Пример даты рождения
        };

        var pozdrikIdDto = new PozdrikIdDto { PozdrikId = null };
        this.mockService.Setup(s => s.GetPozdrikId(friendDto)).ReturnsAsync(pozdrikIdDto);

        // Act
        var result = await this.controller.GetPozdrikIdForMail(friendDto);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetTodayBirthdays_ShouldReturnOk_WhenManHave()
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
        this.mockService.Setup(s => s.SelectForTodayBithrday()).ReturnsAsync(friends);

        // Act
        var res = await this.controller.GetTodayBirthdays();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(res);
        var returnedTodayBirthdays = Assert.IsType<List<FriendList>>(okResult.Value);
        Assert.Single(returnedTodayBirthdays);
        Assert.Equal(friendList.FriendUsername, returnedTodayBirthdays.First().FriendUsername);
    }

    [Fact]
    public async Task GetCongrStringByIdPozdrik_ShouldReturnAllGood()
    {
        // Arrange
        var pozdrID = 1;
        var expectedPozdrik = "Happy bday";
        this.mockService.Setup(s => s.SelectPozdStringAsync(It.IsAny<PozdrikIdDto>())).ReturnsAsync(expectedPozdrik);

        // Act
        var result = await this.controller.GetCongrStr(pozdrID);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(expectedPozdrik, okResult.Value);
    }

    [Fact]
    public async Task GetCongrStr_ShouldReturnNotFound_WhenPozdrikDoesNotExist()
    {
        // Arrange
        var pozdrikId = 999;
        this.mockService.Setup(s => s.SelectPozdStringAsync(It.IsAny<PozdrikIdDto>()))
            .ReturnsAsync((string)null); // Возвращаем null, если поздравление не найдено

        // Act
        var result = await this.controller.GetCongrStr(pozdrikId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetEmail_ShouldReturnOk_WhenEmailExists()
    {
        // Arrange
        var appId = Guid.NewGuid();
        var expectedEmail = "example@example.com";

        this.mockService.Setup(s => s.GetEmailAsync(It.IsAny<AppIdDto>()))
            .ReturnsAsync(expectedEmail);

        // Act
        var result = await this.controller.GetEmail(appId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(expectedEmail, okResult.Value);
    }
}
