using Domain.Models;

namespace Domain.Interfaces;

public interface IFriendRepository
{
    Task<FriendList> AddFriendInListAsync(FriendList friendList);
    Task<FriendList> DeleteFriendFromListAsync(FriendList friendList);
    Task<FriendList> UpdateFriendInListAsync(FriendList friendList);
    Task<List<FriendList>> SelectByAppIdAsync(Guid appid);
    Task<int?> GetPozdrikIdAsync( string username, Guid appid);
    Task<List<FriendList>> SelectForTodayBithrdayAsync();
    
    
}