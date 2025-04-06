using System.Threading.Tasks;
using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProxyAPILeval;
using ProxyAPILeval.DTOExample;
using ProxyAPILevel;
using Xunit;

public class TgSubscriptionControllerTests
{
    private readonly Mock<ITgSubscriptionService> _mockService;
    private readonly TgSubscriptionController _controller;

    public TgSubscriptionControllerTests()
    {
        _mockService = new Mock<ITgSubscriptionService>();
        _controller = new TgSubscriptionController(_mockService.Object);
    }

    [Fact]
    public async Task Subscribe_WhenSubscriptionIsSuccessful_ReturnsOk()
    {
        // Arrange
        var dto = new RegisterTgDto { AppId = Guid.NewGuid(), TgId = 123456789 };
        _mockService.Setup(s => s.SubscribeAsync(dto)).ReturnsAsync(true);

        // Act
        var result = await _controller.Subscribe(dto).ConfigureAwait(false);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);

    }

    [Fact]
    public async Task Subscribe_WhenSubscriptionFails_ReturnsBadRequest()
    {
        // Arrange
        var dto = new RegisterTgDto { AppId = Guid.NewGuid(), TgId = 123456789 };
        _mockService.Setup(s => s.SubscribeAsync(dto)).ReturnsAsync(false);

        // Act
        var result = await _controller.Subscribe(dto).ConfigureAwait(false);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

    }
}