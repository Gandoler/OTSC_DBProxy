using Domain.Models;

namespace Domain.Interfaces.IServices;

public interface IPasswordRecoveryService
{
    Task<Guid> GetIdByMailAsync(string mail);
    
    Task<bool> ExicstCheckByLoginAsync(string login);
    
}