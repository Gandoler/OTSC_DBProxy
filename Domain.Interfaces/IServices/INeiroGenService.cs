using Domain.Models;
using Entities.Templates;

namespace Domain.Interfaces.IServices;

public interface INeiroGenService
{
    Task<Pozdrik> SelectIntAndPozhAsync(PozdrikIdDto pozdrikId);
    
    Task<bool> AddPozdrAsync(PozdrStringDTO pozdr);
}