using Domain.DTO.DTO.MailComp;
using Domain.Models;
using Entities.Templates;

namespace Domain.Interfaces.IServices;

public interface IRegistrService
{
    Task<bool> RegisterAsync(RegisterDto dto);
    Task<bool> ExicstCheckAsync(CheckExistDto dto);
    Task<bool> AddMail(ADDMailDto addMailDto);
    Task<AppIdDto?> GetAppId(CheckExistDto dto);

}