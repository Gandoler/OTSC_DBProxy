using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProxyAPILevel;

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
        _mock.Setup(s=>s.GetIdByMailAsync(email)).ReturnsAsync(appIdDto);
        
        //Act

        var result= await _controller.GetIdByEmail(email);
        //Assert
        var okRes= Assert.IsType<OkObjectResult>(result);
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
        _mock.Setup(s=>s.GetIdByMailAsync(email)).ReturnsAsync((AppIdDto?)null);
        
        //Act

        var result= await _controller.GetIdByEmail(email);
        //Assert
        var okRes = Assert.IsType<OkObjectResult>(result);
        Assert.Null(okRes.Value);
    }
    
    
}