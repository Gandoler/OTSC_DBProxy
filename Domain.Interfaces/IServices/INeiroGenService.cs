using Domain.DTO.DTO.Pozdr;
using Domain.Models;
using Entities.Templates;

namespace Domain.Interfaces.IServices;

public interface INeiroGenService
{
    Task<AddIntAndPozhDto> SelectIntAndPozhAsync(PozdrikIdDto pozdrikId);
    
    Task<bool> AddPozdrAsync(PozdrStringDTO pozdr);
    
    Task<string?> GetNameByPozdrikId( PozdrikIdDto pozdrikId);
    Task<string?> GetUserNameByPozdrikId( PozdrikIdDto pozdrikId);
    Task<List<int>> SelectEmptyOrNullPozdrstringAsync();
}