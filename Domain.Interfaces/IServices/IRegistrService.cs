using Domain.Models;
using Entities.Templates;

namespace Domain.Interfaces.IServices;

public interface IRegistrService
{
    Task<bool> RegisterAsync(RegisterDto dto);
    Task<bool> ExicstCheckAsync(CheckExistDto dto);
}