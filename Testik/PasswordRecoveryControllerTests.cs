using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProxyAPILevel;
using Serilog;

namespace Testik;

public class PasswordRecoveryControllerTests
{
    private readonly Mock<IPasswordRecoveryService> _mock;
    private readonly PasswordRecoveryController _controller;

    public PasswordRecoveryControllerTests()
    {
        _mock = new Mock<IPasswordRecoveryService>();
        _controller = new PasswordRecoveryController(_mock.Object);
    }
    [Fact]
    public async Task GetIdByEmail_MustReturnAppIdDto()
    {
        //Arrange
        var email = "trokhin87@gmail.com";
        var appIdDto = new AppIdDto()
        {
            AppId = Guid.NewGuid()
        };
        _mock.Setup(s => s.GetIdByMailAsync(email)).ReturnsAsync(appIdDto);

        //Act

        var result = await _controller.GetIdByEmail(email).ConfigureAwait(false);
        //Assert
        var okRes = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(appIdDto, okRes.Value);
    }

    [Fact]
    public async Task GetIdByEmail_MustReturnFalse()
    {
        //Arrange
        var email = "trokhin87@gmail.com";
        var appIdDto = new AppIdDto()
        {
            AppId = Guid.NewGuid()
        };
        _mock.Setup(s => s.GetIdByMailAsync(email)).ReturnsAsync((AppIdDto?)null);

        //Act

        var result = await _controller.GetIdByEmail(email).ConfigureAwait(false);
        //Assert
        var okRes = Assert.IsType<OkObjectResult>(result);
        Assert.Null(okRes.Value);
    }

    [Fact]
    public async Task GetLoginByMail_MustReturnAppIdDto()
    {
        var email = "trokhin87@gmail.com";
        string? login = "zhenek";
        _mock.Setup(s => s.GetLoginByMailAsync(email)).ReturnsAsync(login);

        var result = await _controller.GetLoginByMail(email).ConfigureAwait(false);

        var okRes = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(login, okRes.Value);
    }

    [Fact]
    public async Task CheckExistMail_MustReturnTrue()
    {
        var email = "trokhin87@gmail.com";
        _mock.Setup(s => s.ExistByMailAsync(email)).ReturnsAsync(true);

        var result = await _controller.CheckMail(email).ConfigureAwait(false);

        var okRes = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(true, okRes.Value);
    }
    [Fact]
    public async Task CheckExistLogin_MustReturnTrue()
    {
        var email = "trokhin87";
        _mock.Setup(s => s.ExicstCheckByLoginAsync(email)).ReturnsAsync(true);

        var result = await _controller.CheckUserExists(email).ConfigureAwait(false);

        var okRes = Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdatePassword_MustReturnTrue()
    {
        // Arrange
        var loginDto = new LoginDto()
        {
            Login = "trokhin87",
            Password = "1234"
        };

        // Настраиваем mock сервиса возвращать true (успешное обновление)
        _mock.Setup(s => s.UpdateAsync(loginDto)).ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateUser(loginDto).ConfigureAwait(false);

        var okResult = Assert.IsType<OkObjectResult>(result);

    }

}