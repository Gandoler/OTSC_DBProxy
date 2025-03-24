using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DATA;
using Microsoft.EntityFrameworkCore;

namespace UseCases.Repositoties;

public class MailComprRepository : IMailComprRepository
{
    private readonly ApplicationContext _context;

    public MailComprRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<bool> AddMailAsync(Guid appid, string mail)
    {
        if (await _context.Set<MailComprehension>().AnyAsync(x => x.Mail == mail))
        {
            return false; 
        }


        var mailComprehension = new MailComprehension { Appid = appid, Mail = mail };
        _context.Set<MailComprehension>().Add(mailComprehension);
        int affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0; 
    }

    public async Task<Guid> GetIdByMailAsync(string mail)
    {
        return await _context.Set<MailComprehension>()
            .Where(f => f.Mail == mail)
            .Select(f => f.Appid)
            .FirstOrDefaultAsync();
    }
    
    public async Task<string?> GetMailByIdAsync(Guid appid)
    {
        return await _context.Set<MailComprehension>()
            .Where(f => f.Appid == appid)
            .Select(f => f.Mail)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> ExistByMailAsync(string mail)
    {
        return await _context.MailComprehensions.AnyAsync(x => x.Mail == mail);
    }
}