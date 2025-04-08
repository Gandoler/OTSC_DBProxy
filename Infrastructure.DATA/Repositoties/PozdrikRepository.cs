// <copyright file="PozdrikRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UseCases;

using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DATA;
using Microsoft.EntityFrameworkCore;

public class PozdrikRepository : IPozdrikRepository
{
    private readonly ApplicationContext context;

    public PozdrikRepository(ApplicationContext context)
    {
        this.context = context;
    }

    public async Task<bool> AddIntAndPozhAsync(int idPozdr, string? interests, string? pozhelania)
    {
        Pozdrik pozdrik;
        var exists = await this.context.Set<Pozdrik>().AnyAsync(p => p.IdPozdr == idPozdr).ConfigureAwait(false);
        if (exists)
        {
            pozdrik = new Pozdrik { IdPozdr = idPozdr, Interest = interests, Pozhelanie = pozhelania };
            this.context.Set<Pozdrik>().Update(pozdrik);
            return await this.context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        pozdrik = new Pozdrik { IdPozdr = idPozdr, Interest = interests, Pozhelanie = pozhelania };
        this.context.Set<Pozdrik>().Add(pozdrik);
        int req = await this.context.SaveChangesAsync().ConfigureAwait(false);
        return req > 0;
    }

    public async Task<int> CreatePozdrikAsync(string? interests, string? pozhelania)
    {
        var pozdrik = new Pozdrik { Interest = interests, Pozhelanie = pozhelania };
        this.context.Set<Pozdrik>().Add(pozdrik);
        await this.context.SaveChangesAsync().ConfigureAwait(false);
        return pozdrik.IdPozdr;
    }

    public async Task<(string?, string?)> SelectIntAndPozhAsync(int? idPozdr)
    {
        Pozdrik? pozd = await this.context.Set<Pozdrik>().Where(f => f.IdPozdr == idPozdr).FirstOrDefaultAsync().ConfigureAwait(false);
        return (pozd?.Interest, pozd?.Pozhelanie);
    }

    public async Task<string?> SelectPozdrikAsync(int? idPozdr)
    {
        Pozdrik? pozd = await this.context.Set<Pozdrik>().Where(f => f.IdPozdr == idPozdr).FirstOrDefaultAsync().ConfigureAwait(false);
        return pozd?.Textpozdr;
    }

    public async Task<bool> AddPozdrAsync(int idPozdr, string? pozdrtext)
    {
        Pozdrik? zapis = await this.context.Set<Pozdrik>().Where(f => f.IdPozdr == idPozdr).FirstOrDefaultAsync().ConfigureAwait(false);
        if (zapis != null)
        {
            zapis.Textpozdr = pozdrtext;
            this.context.Set<Pozdrik>().Update(zapis);
            int res = await this.context.SaveChangesAsync().ConfigureAwait(false);
            return res > 0;
        }

        return false;
    }

    public async Task<List<int>> SelectEmptyOrNullPozdrstringAsync()
    {
        return await this.context.Set<Pozdrik>()
            .Where(f => string.IsNullOrEmpty(f.Textpozdr))
            .Select(f => f.IdPozdr)
            .ToListAsync().ConfigureAwait(false);

    }
}
