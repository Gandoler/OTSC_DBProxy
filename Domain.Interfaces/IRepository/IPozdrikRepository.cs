using Domain.Models;

namespace Domain.Interfaces;

public interface IPozdrikRepository
{
    Task<bool> AddIntAndPozhAsync(int idPozdr,string interests, string pozhelania);
    Task<Pozdrik> SelectIntAndPozhAsync(int idPozdr);
    Task<Pozdrik> SelectPozdrikAsync(int idPozdr);
    Task<bool> AddPozdrAsync(int idPozdr, string pozdr);
}