// <copyright file="MailComprRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UseCases.Repositoties;

using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DATA;
using Microsoft.EntityFrameworkCore;

public class MailComprRepository : IMailComprRepository
{
    private readonly ApplicationContext context;

    public MailComprRepository(ApplicationContext context)
    {
        this.context = context;
    }

    public async Task<bool> AddMailAsync(Guid appid, string mail)
    {
        if (await this.context.Set<MailComprehension>().AnyAsync(x => x.Mail == mail).ConfigureAwait(false))
        {
            return false;
        }

        var mailComprehension = new MailComprehension { Appid = appid, Mail = mail };
        this.context.Set<MailComprehension>().Add(mailComprehension);
        int affectedRows = await this.context.SaveChangesAsync().ConfigureAwait(false);
        return affectedRows > 0;
    }

    public async Task<Guid> GetIdByMailAsync(string mail)
    {
        return await this.context.Set<MailComprehension>()
            .Where(f => f.Mail == mail)
            .Select(f => f.Appid)
            .FirstOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task<string?> GetMailByIdAsync(Guid appid)
    {
        return await this.context.Set<MailComprehension>()
            .Where(f => f.Appid == appid)
            .Select(f => f.Mail)
            .FirstOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task<bool> ExistByMailAsync(string mail)
    {
        return await this.context.MailComprehensions.AnyAsync(x => x.Mail == mail).ConfigureAwait(false);
    }
}
