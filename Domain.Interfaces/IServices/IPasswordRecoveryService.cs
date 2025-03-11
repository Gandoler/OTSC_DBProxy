using Domain.Models;
using Entities.Templates;

namespace Domain.Interfaces.IServices;

public interface IPasswordRecoveryService
{
    Task<Guid> GetIdByMailAsync(string mail);
    
    Task<bool> ExicstCheckByLoginAsync(string login);
    
    Task<User> UpdateAsync(LoginDto loginDto);
}