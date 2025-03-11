using Domain.Models;

namespace Domain.Interfaces.IServices;

public interface INeiroGenService
{
    Task<Pozdrik> SelectIntAndPozhAsync(int idPozdr);
    
    Task<bool> AddPozdrAsync(int idPozdr,string pozdr);
}