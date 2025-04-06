using System;
using System.Threading.Tasks;
using Domain.DTO.DTO.Friend;
using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProxyAPILevel;
using ProxyAPILeval.DTOExample;
using Xunit;

namespace Testik;
public class FriendControllerTests
{
    private readonly Mock<IFriendService> _friendServiceMock;
    private readonly FriendController _friendController;

    public FriendControllerTests()
    {
        _friendServiceMock = new Mock<IFriendService>();
        _friendController = new FriendController(_friendServiceMock.Object);
    }

    [Fact]
    public async Task AddFriend_Success_ReturnsOk()
    {
        var friendDto = new FriendDto { AppId = Guid.NewGuid(), FriendUsername = "test_friend" };
        _friendServiceMock.Setup(s => s.AddFriendInListAsync(friendDto)).ReturnsAsync(true);

        var result = await _friendController.AddFriend(friendDto).ConfigureAwait(false) as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task DeleteFriend_Success_ReturnsOk()
    {
        var deleteFriendDto = new DeleteFriendDto { AppId = Guid.NewGuid(), FriendUsername = "test_friend" };
        _friendServiceMock.Setup(s => s.DeleteFriendFromListAsync(deleteFriendDto)).ReturnsAsync(true);

        var result = await _friendController.DeleteFriend(deleteFriendDto).ConfigureAwait(false) as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task GetFriendsByAppId_ReturnsOk_WithFriendsList()
    {
        var appId = Guid.NewGuid();
        var expectedFriends = new List<FriendDto> { new FriendDto { AppId = appId, FriendUsername = "test_friend" } };
        _friendServiceMock.Setup(s => s.SelectByAppIdAsync(It.IsAny<AppIdDto>())).ReturnsAsync(expectedFriends);

        var result = await _friendController.GetFriendsByAppId(appId).ConfigureAwait(false) as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(expectedFriends, result.Value);
    }
}
