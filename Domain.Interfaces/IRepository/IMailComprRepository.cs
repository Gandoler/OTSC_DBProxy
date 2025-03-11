using System.Net.Mail;
using Domain.Models;

namespace Domain.Interfaces;

public interface IMailComprRepository
{
    Task<MailComprehension> AddMailAsync(Guid appid, string mail);
    Task<Guid> GetIdByMailAsync(string mail);
}