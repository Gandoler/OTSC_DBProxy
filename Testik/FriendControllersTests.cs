// <copyright file="FriendControllersTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Testik;

using System;
using System.Threading.Tasks;
using Domain.DTO.DTO.Friend;
using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProxyAPILeval.DTOExample;
using ProxyAPILevel;
using Xunit;

public class FriendControllerTests
{
    private readonly Mock<IFriendService> friendServiceMock;
    private readonly FriendController friendController;

    public FriendControllerTests()
    {
        this.friendServiceMock = new Mock<IFriendService>();
        this.friendController = new FriendController(this.friendServiceMock.Object);
    }

    [Fact]
    public async Task AddFriend_Success_ReturnsOk()
    {
        var friendDto = new FriendDto { AppId = Guid.NewGuid(), FriendUsername = "test_friend" };
        this.friendServiceMock.Setup(s => s.AddFriendInListAsync(friendDto)).ReturnsAsync(true);

        var result = await this.friendController.AddFriend(friendDto)as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task DeleteFriend_Success_ReturnsOk()
    {
        var deleteFriendDto = new DeleteFriendDto { AppId = Guid.NewGuid(), FriendUsername = "test_friend" };
        this.friendServiceMock.Setup(s => s.DeleteFriendFromListAsync(deleteFriendDto)).ReturnsAsync(true);

        var result = await this.friendController.DeleteFriend(deleteFriendDto)as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task GetFriendsByAppId_ReturnsOk_WithFriendsList()
    {
        var appId = Guid.NewGuid();
        var expectedFriends = new List<FriendDto> { new FriendDto { AppId = appId, FriendUsername = "test_friend" } };
        this.friendServiceMock.Setup(s => s.SelectByAppIdAsync(It.IsAny<AppIdDto>())).ReturnsAsync(expectedFriends);

        var result = await this.friendController.GetFriendsByAppId(appId)as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(expectedFriends, result.Value);
    }
}
