using System.Net.Mail;
using Domain.Models;

namespace Domain.Interfaces;

public interface IMailComprRepository
{
    Task<bool> AddMailAsync(Guid appid, string mail);
    Task<Guid> GetIdByMailAsync(string mail);
    
    Task <string?> GetMailByIdAsync(Guid appid);
    Task<bool> ExistByMailAsync(string mail);
}