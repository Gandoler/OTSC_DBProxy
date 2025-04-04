using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DTO.DTO.Friend;
using Domain.DTO.DTO.MailComp;
using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProxyAPILevel;
using Xunit;

public class RegisterControllerTests
{
    private readonly Mock<IRegistrService> _mockService;
    private readonly RegisterController _controller;

    public RegisterControllerTests()
    {
        _mockService = new Mock<IRegistrService>();
        _controller = new RegisterController(_mockService.Object);
    }

    [Fact]
    public async Task RegisterUser_WhenRegistrationSucceeds_ReturnsOk()
    {
        // Arrange
        var registerDto = new RegisterDto
        {
            Login = "ZHENek",
            Password = "123456",
            Email = "email@mail.com",
        };
        _mockService.Setup(s => s.RegisterAsync(registerDto)).ReturnsAsync(true);

        // Act
        var result = await _controller.RegisterUser(registerDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task RegisterUser_WhenRegistrationFails_ReturnsBadRequest()
    {
        // Arrange
        var registerDto = new RegisterDto { 
            Login = "ZHENek",
            Password = "123456",
            Email = "email@mail.com"
            
        };
        _mockService.Setup(s => s.RegisterAsync(registerDto)).ReturnsAsync(false);

        // Act
        var result = await _controller.RegisterUser(registerDto);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        
    }

    [Fact]
    public async Task CheckUserExists_WhenUserExists_ReturnsOk()
    {
        // Arrange
        string email = "test@example.com";
        _mockService.Setup(s => s.ExicstCheckAsync(It.IsAny<CheckExistDto>())).ReturnsAsync(true);

        // Act
        var result = await _controller.CheckUserExists(email);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
       
    }

    [Fact]
    public async Task CheckUserExists_WhenUserDoesNotExist_ReturnsBadRequest()
    {
        // Arrange
        string email = "test@example.com";
        _mockService.Setup(s => s.ExicstCheckAsync(It.IsAny<CheckExistDto>())).ReturnsAsync(false);

        // Act
        var result = await _controller.CheckUserExists(email);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        
    }

    [Fact]
    public async Task GetUserIdByLogin_WhenUserExists_ReturnsOkWithAppId()
    {
        // Arrange
        string login = "testUser";
        var expectedAppId = new AppIdDto { AppId = Guid.NewGuid() };
        _mockService.Setup(s => s.GetAppId(It.IsAny<CheckExistDto>())).ReturnsAsync(expectedAppId);

        // Act
        var result = await _controller.GetUserIdByLogin(login);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var appIdDto = Assert.IsType<AppIdDto>(okResult.Value);
        Assert.Equal(expectedAppId.AppId, appIdDto.AppId);
    }

    [Fact]
    public async Task GetUserIdByLogin_WhenUserDoesNotExist_ReturnsBadRequest()
    {
        // Arrange
        string login = "testUser";
        _mockService.Setup(s => s.GetAppId(It.IsAny<CheckExistDto>())).ReturnsAsync((AppIdDto)null);

        // Act
        var result = await _controller.GetUserIdByLogin(login);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
       
    }

    [Fact]
    public async Task AddMail_WhenMailAddedSuccessfully_ReturnsOk()
    {
        // Arrange
        var mailDto = new ADDMailDto { /* Заполни данными */ };
        _mockService.Setup(s => s.AddMail(mailDto)).ReturnsAsync(true);

        // Act
        var result = await _controller.AddMail(mailDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
       
    }

    [Fact]
    public async Task AddMail_WhenMailAlreadyExists_ReturnsBadRequest()
    {
        // Arrange
        var mailDto = new ADDMailDto { /* Заполни данными */ };
        _mockService.Setup(s => s.AddMail(mailDto)).ReturnsAsync(false);

        // Act
        var result = await _controller.AddMail(mailDto);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
       
    }
}
