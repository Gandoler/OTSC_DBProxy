using Domain.DTO.DTO.Friend;
using Domain.DTO.DTO.Pozdr;
using Domain.Models;
using Entities.Templates;

namespace Domain.Interfaces.IServices;

public interface IFriendService
{
    // с френдрепизитория
    Task<bool> AddFriendInListAsync(FriendDto  friend);
    Task<bool> DeleteFriendFromListAsync(DeleteFriendDto friend);
    Task<bool> UpdateFriendInListAsync(FriendDto  friend);
    Task<List<FriendDto>> SelectByAppIdAsync( AppIdDto appid);
    Task<PozdrikIdDto> GetPozdrikIdAsync(GetPozdrikQueryDto queryDto);
  
    
    
    // c поздрик репозитория
    Task<bool> AddIntAndPozhAsync(AddIntAndPozhDto intAndPozh);
    Task<AddIntAndPozhDto> SelectIntAndPozhAsync(PozdrikIdDto pozdrik);
    
}