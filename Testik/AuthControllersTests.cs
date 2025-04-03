using System.Threading.Tasks;
using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProxyAPILevel;
using Xunit;

namespace Testik;

public class AuthControllerTests
{
    private readonly Mock<IAuthService> _authServiceMock;
    private readonly AuthController _authController;

    public AuthControllerTests()
    {
        _authServiceMock = new Mock<IAuthService>();
        _authController = new AuthController(_authServiceMock.Object);
    }

    [Fact]
    public async Task CheckUserExists_UserExists_ReturnsTrue()
    {
        // Arrange
        var loginDto = new LoginDto { Login = "user1", Password = "password1" };
        _authServiceMock.Setup(s => s.ExicstCheckAsync(loginDto)).ReturnsAsync(true);
        
        // Act
        var result = await _authController.CheckUserExists(loginDto) as OkObjectResult;
        
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
        _authServiceMock.Setup(s => s.ExicstCheckAsync(loginDto)).ReturnsAsync(false);
        //
        // Act
        var result = await _authController.CheckUserExists(loginDto) as OkObjectResult;
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.False((bool)result.Value.GetType().GetProperty("exists")?.GetValue(result.Value));
    }
}
