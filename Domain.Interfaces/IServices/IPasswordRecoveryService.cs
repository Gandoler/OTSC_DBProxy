using Domain.Models;
using Entities.Templates;

namespace Domain.Interfaces.IServices;

public interface IPasswordRecoveryService
{
    Task<AppIdDto> GetIdByMailAsync(string mail);
    
    Task<bool> ExicstCheckByLoginAsync(string login);
    
    Task<bool> ExistByMailAsync(string mail);
    
    Task<bool> UpdateAsync(LoginDto loginDto);
}