using Domain.Models;
using Entities.Templates;

namespace Domain.Interfaces.IServices;

public interface IFriendService
{
    // с френдрепизитория
    Task<bool> AddFriendInListAsync(FriendDto  friend);
    Task<bool> DeleteFriendFromListAsync(AppIdDto appid);
    Task<bool> UpdateFriendInListAsync(FriendDto  friend);
    Task<List<FriendList>> SelectByAppIdAsync( AppIdDto appid);
    Task<PozdrikIdDto> GetPozdrikIdAsync(GetPozdrikQueryDto queryDto);
  
    
    
    // c поздрик репозитория
    Task<bool> AddIntAndPozhAsync(AddIntAndPozhDto intAndPozh);
    Task<Pozdrik> SelectIntAndPozhAsync(PozdrikIdDto pozdrik);
    
}