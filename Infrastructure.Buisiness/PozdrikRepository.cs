using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace UseCases;

public class PozdrikRepository: IPozdrikRepository
{
    
    private readonly DbContext _context;

    public PozdrikRepository(DbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> AddIntAndPozhAsync(int idPozdr, string interests, string pozhelania)
    {
        Pozdrik pozdrik = new Pozdrik{IdPozdr = idPozdr, Interest = interests,Pozhelanie = pozhelania};
       _context.Set<Pozdrik>().Add(pozdrik);
       int req =  await _context.SaveChangesAsync();
       return req > 0;

    }

    public async Task<(string?, string?)> SelectIntAndPozhAsync(int idPozdr)
    {
        Pozdrik? pozd =  await _context.Set<Pozdrik>().Where(f=>f.IdPozdr == idPozdr).FirstOrDefaultAsync();
        return (pozd?.Interest, pozd?.Pozhelanie);
    }

    public async Task<string?> SelectPozdrikAsync(int idPozdr)
    {
        Pozdrik? pozd =  await _context.Set<Pozdrik>().Where(f=>f.IdPozdr == idPozdr).FirstOrDefaultAsync();
        return pozd?.Textpozdr;
    }

    public async Task<bool> AddPozdrAsync(int idPozdr, string pozdrtext)
    {
        Pozdrik? zapis = await _context.Set<Pozdrik>().Where(f=>f.IdPozdr == idPozdr).FirstOrDefaultAsync();
        if (zapis != null)
        {
           zapis.Textpozdr = pozdrtext;
           _context.Set<Pozdrik>().Update(zapis);
           int res = await _context.SaveChangesAsync();
           return res > 0;
        }

        return false;


    }
}