using System.Net.Mail;
using Domain.Models;

namespace Domain.Interfaces;

public interface IMailComprRepository
{
    Task<MailComprehension> AddMail(Guid appid, string mail);
    Task<Guid> GETIdByMail(Guid appid);
}