using Domain.Models;
using Entities.Templates;

namespace Domain.Interfaces;

public interface IFriendRepository
{
    Task<bool> AddFriendInListAsync(FriendList friendList);
    Task<bool> DeleteFriendFromListAsync(Guid appId, string friendUsername);
    Task<bool> UpdateFriendInListAsync(FriendList friendList);
    Task<List<FriendList>> SelectByAppIdAsync(Guid appid);
    Task<int?> GetPozdrikIdAsync( string? username, Guid appid);
    Task<List<FriendList>> SelectForTodayBithrdayAsync();
    
    Task<string?> GetFrienNameByPozdrikId(int? pozdrikId);
    Task<string?> GetFrienUserNameByPozdrikId(int? pozdrikId);
    
    
}