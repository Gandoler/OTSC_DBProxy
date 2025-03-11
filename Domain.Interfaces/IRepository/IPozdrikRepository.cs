using Domain.Models;

namespace Domain.Interfaces;

public interface IPozdrikRepository
{
    Task<Pozdrik> AddIntAndPozh(Pozdrik pozdrik);
    Task<Pozdrik> SelectPozdrik(int idPozdr);
    Task<Pozdrik> AddPozdr(string pozdr);
}