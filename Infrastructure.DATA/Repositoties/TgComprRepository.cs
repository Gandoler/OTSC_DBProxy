// <copyright file="TgComprRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UseCases.Repositoties;

using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DATA;
using Microsoft.EntityFrameworkCore;

public class TgComprRepository : ITgComprRepository
{
    private readonly ApplicationContext context;

    public TgComprRepository(ApplicationContext context)
    {
        this.context = context;
    }

    public async Task<bool> AddTgAsync(Guid appid, long telegramId)
    {
        if (await this.context.Set<TgComprehension>().AnyAsync(x => x.TgId == telegramId).ConfigureAwait(false))
        {
            return false; // Запись уже существует
        }

        var zapiszTg = new TgComprehension { Appid = appid, TgId = telegramId };
        this.context.Set<TgComprehension>().Add(zapiszTg);
        int result = await this.context.SaveChangesAsync().ConfigureAwait(false);
        return result > 0;
    }

    public async Task<Guid> GetIdByTgAsync(long tgId)
    {
        return await this.context.Set<TgComprehension>()
            .Where(f => f.TgId == tgId)
            .Select(f => f.Appid)
            .FirstOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task<long?> GetTgId(Guid appId)
    {
        return await this.context.Set<TgComprehension>()
            .Where(f => f.Appid == appId)
            .Select(f => (long?)f.TgId)
            .FirstOrDefaultAsync().ConfigureAwait(false);
    }
}
