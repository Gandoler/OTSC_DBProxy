// <copyright file="NeiroGenControllersTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DTO.DTO.Pozdr;
using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProxyAPILevel.Controllers;
using Xunit;

public class NeiroGenControllerTests
{
    private readonly Mock<INeiroGenService> mockService;
    private readonly NeiroGenController controller;

    public NeiroGenControllerTests()
    {
        this.mockService = new Mock<INeiroGenService>();
        this.controller = new NeiroGenController(this.mockService.Object);
    }

    [Fact]
    public async Task GetIntAndPozh_ShouldReturnOk_WhenPozdrikExists()
    {
        // Arrange
        var pozdrikId = 1;
        var pozdrikDto = new PozdrStringDTO { PozdrikId = pozdrikId, Pozdr = "Поздравление" };
        var addIntAndPozh = new AddIntAndPozhDto()
        {
            IdPozdr = pozdrikId,
            Pozhelania = "pozhelania",
            Interests = "interests",
        };
        this.mockService.Setup(s => s.SelectIntAndPozhAsync(It.IsAny<PozdrikIdDto>()))
                    .ReturnsAsync(addIntAndPozh);

        // Act
        var result = await this.controller.GetIntAndPozh(pozdrikId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(addIntAndPozh, okResult.Value);
    }

    [Fact]
    public async Task AddPozdrik_ShouldReturnOk_WhenPozdrikAddedSuccessfully()
    {
        // Arrange
        var pozdrikDto = new PozdrStringDTO { PozdrikId = 1, Pozdr = "С Днем Рождения!" };

        this.mockService.Setup(s => s.AddPozdrAsync(pozdrikDto))
            .ReturnsAsync(true);

        // Act
        var result = await this.controller.AddPozdrik(pozdrikDto);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task AddPozdrik_ShouldReturnBadRequest_WhenPozdrikAdditionFails()
    {
        // Arrange
        var pozdrikDto = new PozdrStringDTO { PozdrikId = 3, Pozdr = "Удачи!" };

        this.mockService.Setup(s => s.AddPozdrAsync(pozdrikDto))
                    .ReturnsAsync(false);

        // Act
        var result = await this.controller.AddPozdrik(pozdrikDto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetNameByPozdrikId_ShouldReturnOk_WhenNameExists()
    {
        // Arrange
        var pozdrikId = 4;
        var name = "Иван";

        this.mockService.Setup(s => s.GetNameByPozdrikId(It.IsAny<PozdrikIdDto>()))
                    .ReturnsAsync(name);

        // Act
        var result = await this.controller.GetNameByPozdrikId(pozdrikId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(name, okResult.Value);
    }

    [Fact]
    public async Task GetNameByPozdrikId_ShouldReturnNotFound_WhenNameDoesNotExist()
    {
        // Arrange
        var pozdrikId = 5;

        this.mockService.Setup(s => s.GetNameByPozdrikId(It.IsAny<PozdrikIdDto>()))
                    .ReturnsAsync((string)null);

        // Act
        var result = await this.controller.GetNameByPozdrikId(pozdrikId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetUserNameByPozdrikId_ShouldReturnOk_WhenUsernameExists()
    {
        // Arrange
        var pozdrikId = 6;
        var username = "user123";

        this.mockService.Setup(s => s.GetUserNameByPozdrikId(It.IsAny<PozdrikIdDto>()))
                    .ReturnsAsync(username);

        // Act
        var result = await this.controller.GetUserNameByPozdrikId(pozdrikId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(username, okResult.Value);
    }

    [Fact]
    public async Task GetUserNameByPozdrikId_ShouldReturnNotFound_WhenUsernameDoesNotExist()
    {
        // Arrange
        var pozdrikId = 7;

        this.mockService.Setup(s => s.GetUserNameByPozdrikId(It.IsAny<PozdrikIdDto>()))
                    .ReturnsAsync((string)null);

        // Act
        var result = await this.controller.GetUserNameByPozdrikId(pozdrikId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
