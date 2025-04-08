// <copyright file="AuthControllersTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Testik;

using System.Threading.Tasks;
using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProxyAPILevel;
using Xunit;

public class AuthControllerTests
{
    private readonly Mock<IAuthService> authServiceMock;
    private readonly AuthController authController;

    public AuthControllerTests()
    {
        this.authServiceMock = new Mock<IAuthService>();
        this.authController = new AuthController(this.authServiceMock.Object);
    }

    [Fact]
    public async Task CheckUserExists_UserExists_ReturnsTrue()
    {
        // Arrange
        var loginDto = new LoginDto { Login = "user1", Password = "password1" };
        this.authServiceMock.Setup(s => s.ExicstCheckAsync(loginDto)).ReturnsAsync(true);

        // Act
        var result = await this.authController.CheckUserExists(loginDto)as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.True((bool)result.Value.GetType().GetProperty("exists")?.GetValue(result.Value));
    }

    [Fact]
    public async Task CheckUserExists_UserDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var loginDto = new LoginDto { Login = "unknownuser", Password = "password123" };
        this.authServiceMock.Setup(s => s.ExicstCheckAsync(loginDto)).ReturnsAsync(false);

        // Act
        var result = await this.authController.CheckUserExists(loginDto)as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.False((bool)result.Value.GetType().GetProperty("exists")?.GetValue(result.Value));
    }
}
