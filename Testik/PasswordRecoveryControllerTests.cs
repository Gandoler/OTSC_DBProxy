// <copyright file="PasswordRecoveryControllerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Testik;

using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProxyAPILevel;
using Serilog;

public class PasswordRecoveryControllerTests
{
    private readonly Mock<IPasswordRecoveryService> mock;
    private readonly PasswordRecoveryController controller;

    public PasswordRecoveryControllerTests()
    {
        this.mock = new Mock<IPasswordRecoveryService>();
        this.controller = new PasswordRecoveryController(this.mock.Object);
    }

    [Fact]
    public async Task GetIdByEmail_MustReturnAppIdDto()
    {
        // Arrange
        var email = "trokhin87@gmail.com";
        var appIdDto = new AppIdDto()
        {
            AppId = Guid.NewGuid(),
        };
        this.mock.Setup(s => s.GetIdByMailAsync(email)).ReturnsAsync(appIdDto);

        // Act
        var result = await this.controller.GetIdByEmail(email);

        // Assert
        var okRes = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(appIdDto, okRes.Value);
    }

    [Fact]
    public async Task GetIdByEmail_MustReturnFalse()
    {
        // Arrange
        var email = "trokhin87@gmail.com";
        var appIdDto = new AppIdDto()
        {
            AppId = Guid.NewGuid(),
        };
        this.mock.Setup(s => s.GetIdByMailAsync(email)).ReturnsAsync((AppIdDto?)null);

        // Act
        var result = await this.controller.GetIdByEmail(email);

        // Assert
        var okRes = Assert.IsType<OkObjectResult>(result);
        Assert.Null(okRes.Value);
    }

    [Fact]
    public async Task GetLoginByMail_MustReturnAppIdDto()
    {
        var email = "trokhin87@gmail.com";
        string? login = "zhenek";
        this.mock.Setup(s => s.GetLoginByMailAsync(email)).ReturnsAsync(login);

        var result = await this.controller.GetLoginByMail(email);

        var okRes = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(login, okRes.Value);
    }

    [Fact]
    public async Task CheckExistMail_MustReturnTrue()
    {
        var email = "trokhin87@gmail.com";
        this.mock.Setup(s => s.ExistByMailAsync(email)).ReturnsAsync(true);

        var result = await this.controller.CheckMail(email);

        var okRes = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(true, okRes.Value);
    }

    [Fact]
    public async Task CheckExistLogin_MustReturnTrue()
    {
        var email = "trokhin87";
        this.mock.Setup(s => s.ExicstCheckByLoginAsync(email)).ReturnsAsync(true);

        var result = await this.controller.CheckUserExists(email);

        var okRes = Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdatePassword_MustReturnTrue()
    {
        // Arrange
        var loginDto = new LoginDto()
        {
            Login = "trokhin87",
            Password = "1234",
        };

        // Настраиваем mock сервиса возвращать true (успешное обновление)
        this.mock.Setup(s => s.UpdateAsync(loginDto)).ReturnsAsync(true);

        // Act
        var result = await this.controller.UpdateUser(loginDto);

        var okResult = Assert.IsType<OkObjectResult>(result);
    }
}
