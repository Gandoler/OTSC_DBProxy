using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace UseCases.Repositoties;

public class MailComprRepository:IMailComprRepository
{
    private readonly DbContext _context;

    public MailComprRepository(DbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> AddMailAsync(Guid appid, string mail)
    {
        MailComprehension mailComprehension = new MailComprehension { Appid = appid, Mail = mail };
        _context.Set<MailComprehension>().Add(mailComprehension);
        int affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0; // Возвращаем true, если изменения были сохранены
    }

    public async Task<Guid> GetIdByMailAsync(string mail)
    {
        return await _context.Set<MailComprehension>()
            .Where(f => f.Mail == mail)
            .Select(f => f.Appid)
            .FirstOrDefaultAsync();
    }
}