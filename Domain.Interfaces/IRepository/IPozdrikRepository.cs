using Domain.Models;

namespace Domain.Interfaces;

public interface IPozdrikRepository
{
    Task<Pozdrik> AddIntAndPozhAsync(int idPozdr,string interests, string pozhelania);
    Task<Pozdrik> SelectIntAndPozhAsync(int idPozdr);
    Task<Pozdrik> SelectPozdrikAsync(int idPozdr);
    Task<Pozdrik> AddPozdrAsync(string pozdr);
}