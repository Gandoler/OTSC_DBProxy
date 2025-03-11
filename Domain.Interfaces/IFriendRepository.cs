using Domain.Models;

namespace Domain.Interfaces;

public interface IFriendRepository
{
    Task<FriendList> AddFriendInList(FriendList friendList);
    Task<FriendList> DeleteFriendFromList(FriendList friendList);
    Task<FriendList> UpdateFriendInList(FriendList friendList);
    Task<List<FriendList>> SelectByAppId(FriendList friendList);
    Task<int?> GetPozdrikId(FriendList friendList);
    Task<List<FriendList>> SelectByAppId();
    
}