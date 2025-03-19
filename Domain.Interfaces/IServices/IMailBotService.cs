using Domain.DTO.DTO.Friend;
using Domain.DTO.DTO.Pozdr;
using Domain.Models;
using Entities.Templates;

namespace Domain.Interfaces.IServices;

public interface IMailBotService
{
    Task<List<FriendList>> SelectForTodayBithrday();
    
    Task<string?> SelectPozdStringAsync(PozdrikIdDto pozdrikId);
    
    Task<string?> GetEmailAsync(AppIdDto appId);
    Task<PozdrikIdDto> GetPozdrikId(FriendDto friendDto);
}