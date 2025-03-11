using Domain.Models;
using Entities.Templates;

namespace Domain.Interfaces.IServices;

public interface IFriendService
{
    // с френдрепизитория
    Task<FriendList> AddFriendInListAsync(FriendDto  friend);
    Task<FriendList> DeleteFriendFromListAsync(AppIdDto appid);
    Task<FriendList> UpdateFriendInListAsync(FriendDto  friend);
    Task<List<FriendList>> SelectByAppIdAsync( AppIdDto appid);
    Task<PozdrikIdDto> GetPozdrikIdAsync(GetPozdrikQueryDto queryDto);
  
    
    
    // c поздрик репозитория
    Task<Pozdrik> AddIntAndPozhAsync(AddPozdrikDto pozdrik);
    Task<Pozdrik> SelectIntAndPozhAsync(PozdrikIdDto pozdrik);
    
}