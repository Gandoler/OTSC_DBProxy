using Entities.Templates;

namespace Domain.Interfaces.IServices;

public interface IAuthService
{
    Task<bool> ExicstCheckAsync(LoginDto dto);
}