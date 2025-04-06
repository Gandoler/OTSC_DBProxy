using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Templates;
using Domain.DTO.DTO.Pozdr;
using Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProxyAPILevel.Controllers;
using Xunit;

public class NeiroGenControllerTests
{
    private readonly Mock<INeiroGenService> _mockService;
    private readonly NeiroGenController _controller;

    public NeiroGenControllerTests()
    {
        _mockService = new Mock<INeiroGenService>();
        _controller = new NeiroGenController(_mockService.Object);
    }

    [Fact]
    public async Task GetIntAndPozh_ShouldReturnOk_WhenPozdrikExists()
    {
        // Arrange
        var pozdrikId = 1;
        var pozdrikDto = new PozdrStringDTO { _pozdrikId = pozdrikId, _pozdr = "Поздравление" };
        var addIntAndPozh = new AddIntAndPozhDto()
        {
            IdPozdr = pozdrikId,
            Pozhelania = "pozhelania",
            Interests = "interests"
        };
        _mockService.Setup(s => s.SelectIntAndPozhAsync(It.IsAny<PozdrikIdDto>()))
                    .ReturnsAsync(addIntAndPozh);

        // Act
        var result = await _controller.GetIntAndPozh(pozdrikId).ConfigureAwait(false);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(addIntAndPozh, okResult.Value);
    }

    [Fact]
    public async Task AddPozdrik_ShouldReturnOk_WhenPozdrikAddedSuccessfully()
    {
        // Arrange
        var pozdrikDto = new PozdrStringDTO { _pozdrikId = 1, _pozdr = "С Днем Рождения!" };

        _mockService.Setup(s => s.AddPozdrAsync(pozdrikDto))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.AddPozdrik(pozdrikDto).ConfigureAwait(false);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task AddPozdrik_ShouldReturnBadRequest_WhenPozdrikAdditionFails()
    {
        // Arrange
        var pozdrikDto = new PozdrStringDTO { _pozdrikId = 3, _pozdr = "Удачи!" };

        _mockService.Setup(s => s.AddPozdrAsync(pozdrikDto))
                    .ReturnsAsync(false);

        // Act
        var result = await _controller.AddPozdrik(pozdrikDto).ConfigureAwait(false);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetNameByPozdrikId_ShouldReturnOk_WhenNameExists()
    {
        // Arrange
        var pozdrikId = 4;
        var name = "Иван";

        _mockService.Setup(s => s.GetNameByPozdrikId(It.IsAny<PozdrikIdDto>()))
                    .ReturnsAsync(name);

        // Act
        var result = await _controller.GetNameByPozdrikId(pozdrikId).ConfigureAwait(false);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(name, okResult.Value);
    }

    [Fact]
    public async Task GetNameByPozdrikId_ShouldReturnNotFound_WhenNameDoesNotExist()
    {
        // Arrange
        var pozdrikId = 5;

        _mockService.Setup(s => s.GetNameByPozdrikId(It.IsAny<PozdrikIdDto>()))
                    .ReturnsAsync((string)null);

        // Act
        var result = await _controller.GetNameByPozdrikId(pozdrikId).ConfigureAwait(false);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetUserNameByPozdrikId_ShouldReturnOk_WhenUsernameExists()
    {
        // Arrange
        var pozdrikId = 6;
        var username = "user123";

        _mockService.Setup(s => s.GetUserNameByPozdrikId(It.IsAny<PozdrikIdDto>()))
                    .ReturnsAsync(username);

        // Act
        var result = await _controller.GetUserNameByPozdrikId(pozdrikId).ConfigureAwait(false);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(username, okResult.Value);
    }

    [Fact]
    public async Task GetUserNameByPozdrikId_ShouldReturnNotFound_WhenUsernameDoesNotExist()
    {
        // Arrange
        var pozdrikId = 7;

        _mockService.Setup(s => s.GetUserNameByPozdrikId(It.IsAny<PozdrikIdDto>()))
                    .ReturnsAsync((string)null);

        // Act
        var result = await _controller.GetUserNameByPozdrikId(pozdrikId).ConfigureAwait(false);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);

    }

}
