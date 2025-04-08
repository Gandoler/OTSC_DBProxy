// <copyright file="TgSubscriptionControllerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
    private readonly Mock<ITgSubscriptionService> mockService;
    private readonly TgSubscriptionController controller;

    public TgSubscriptionControllerTests()
    {
        this.mockService = new Mock<ITgSubscriptionService>();
        this.controller = new TgSubscriptionController(this.mockService.Object);
    }

    [Fact]
    public async Task Subscribe_WhenSubscriptionIsSuccessful_ReturnsOk()
    {
        // Arrange
        var dto = new RegisterTgDto { AppId = Guid.NewGuid(), TgId = 123456789 };
        this.mockService.Setup(s => s.SubscribeAsync(dto)).ReturnsAsync(true);

        // Act
        var result = await this.controller.Subscribe(dto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Subscribe_WhenSubscriptionFails_ReturnsBadRequest()
    {
        // Arrange
        var dto = new RegisterTgDto { AppId = Guid.NewGuid(), TgId = 123456789 };
        this.mockService.Setup(s => s.SubscribeAsync(dto)).ReturnsAsync(false);

        // Act
        var result = await this.controller.Subscribe(dto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
    }
}
