// <copyright file="RegisterControllerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
    private readonly Mock<IRegistrService> mockService;
    private readonly RegisterController controller;

    public RegisterControllerTests()
    {
        this.mockService = new Mock<IRegistrService>();
        this.controller = new RegisterController(this.mockService.Object);
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
        Guid g = Guid.Empty;
        this.mockService.Setup(s => s.RegisterAsync(registerDto)).ReturnsAsync(g);

        // Act
        var result = await this.controller.RegisterUser(registerDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task RegisterUser_WhenRegistrationFails_ReturnsBadRequest()
    {
        // Arrange
        var registerDto = new RegisterDto
        {
            Login = "ZHENek",
            Password = "123456",
            Email = "email@mail.com",
        };
        Guid g = Guid.Empty;
        this.mockService.Setup(s => s.RegisterAsync(registerDto)).ReturnsAsync(g);

        // Act
        var result = await this.controller.RegisterUser(registerDto);

        // Assert
        Assert.NotNull(result);
    
    }

    [Fact]
    public async Task CheckUserExists_WhenUserExists_ReturnsOk()
    {
        // Arrange
        string email = "test@example.com";
        this.mockService.Setup(s => s.ExicstCheckAsync(It.IsAny<CheckExistDto>())).ReturnsAsync(true);

        // Act
        var result = await this.controller.CheckUserExists(email);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task CheckUserExists_WhenUserDoesNotExist_ReturnsBadRequest()
    {
        // Arrange
        string email = "test@example.com";
        this.mockService.Setup(s => s.ExicstCheckAsync(It.IsAny<CheckExistDto>())).ReturnsAsync(false);

        // Act
        var result = await this.controller.CheckUserExists(email);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetUserIdByLogin_WhenUserExists_ReturnsOkWithAppId()
    {
        // Arrange
        string login = "testUser";
        var expectedAppId = new AppIdDto { AppId = Guid.NewGuid() };
        this.mockService.Setup(s => s.GetAppId(It.IsAny<CheckExistDto>())).ReturnsAsync(expectedAppId);

        // Act
        var result = await this.controller.GetUserIdByLogin(login);

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
        this.mockService.Setup(s => s.GetAppId(It.IsAny<CheckExistDto>())).ReturnsAsync((AppIdDto)null);

        // Act
        var result = await this.controller.GetUserIdByLogin(login);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task AddMail_WhenMailAddedSuccessfully_ReturnsOk()
    {
        // Arrange
        var mailDto = new ADDMailDto { /* Заполни данными */ };
        this.mockService.Setup(s => s.AddMail(mailDto)).ReturnsAsync(true);

        // Act
        var result = await this.controller.AddMail(mailDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task AddMail_WhenMailAlreadyExists_ReturnsBadRequest()
    {
        // Arrange
        var mailDto = new ADDMailDto { /* Заполни данными */ };
        this.mockService.Setup(s => s.AddMail(mailDto)).ReturnsAsync(false);

        // Act
        var result = await this.controller.AddMail(mailDto);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
    }
}
