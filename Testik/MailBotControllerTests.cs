using System;
using System.Threading.Tasks;
using Domain.DTO.DTO.Friend;
using Domain.DTO.DTO.Pozdr;
using Domain.Interfaces.IServices;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProxyAPILevel;
using Xunit;

public class MailBotControllerTests
{
    private readonly Mock<IMailBotService> _mockService;
    private readonly MailBotController _controller;

    public MailBotControllerTests()
    {
        _mockService = new Mock<IMailBotService>();
        _controller = new MailBotController(_mockService.Object);
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
            DateBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-25))
        };

        var pozdrikIdDto = new PozdrikIdDto { _pozdrikId = 100 };
        _mockService.Setup(s => s.GetPozdrikId(friendDto)).ReturnsAsync(pozdrikIdDto);

        // Act
        var result = await _controller.GetPozdrikIdForMail(friendDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPozdrikId = Assert.IsType<PozdrikIdDto>(okResult.Value);
        Assert.Equal(pozdrikIdDto._pozdrikId, returnedPozdrikId._pozdrikId);
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
            DateBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)) // Пример даты рождения
        };

        var pozdrikIdDto = new PozdrikIdDto { _pozdrikId = null };
        _mockService.Setup(s => s.GetPozdrikId(friendDto)).ReturnsAsync(pozdrikIdDto);

        // Act
        var result = await _controller.GetPozdrikIdForMail(friendDto);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetTodayBirthdays_ShouldReturnOk_WhenManHave()
    {
        //Arrange
        var friendList = new FriendList
        {
            Appid = Guid.NewGuid(),
            FriendUsername = "friend123",
            FriendName = "Bob",
            DateBirth = DateOnly.FromDateTime(DateTime.Now), // День рождения сегодня
            IdPozdr = null, // Если IdPozdr не нужно, установите в null
            App = new User { Appid = Guid.NewGuid() }, // Убедитесь, что объект User также инициализирован
            IdPozdrNavigation = null // Если IdPozdrNavigation не нужен, установите в null
        };

        var friends = new List<FriendList> { friendList }; // Добавляем друга в список
        _mockService.Setup(s => s.SelectForTodayBithrday()).ReturnsAsync(friends);
        //Act
        var res=await _controller.GetTodayBirthdays();
        
        //Assert
        var okResult = Assert.IsType<OkObjectResult>(res);
        var returnedTodayBirthdays = Assert.IsType<List<FriendList>>(okResult.Value);
        Assert.Single(returnedTodayBirthdays);
        Assert.Equal(friendList.FriendUsername, returnedTodayBirthdays.First().FriendUsername);
        
        
    }
}
