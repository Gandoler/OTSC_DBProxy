using AutoMapper;
using Domain.DTO.DTO.MailComp;
using Domain.Interfaces;
using Domain.Interfaces.IServices;
using Domain.Models;
using Entities.Templates;

namespace UseCases.Services;

public class RegistrService:IRegistrService
{
    private readonly IUserRepository _userRepository;
    private readonly IMailComprRepository _mailComprRepository;

    public RegistrService(IUserRepository userRepository, IMailComprRepository mailComprRepository)
    {
        _userRepository = userRepository;
        _mailComprRepository = mailComprRepository;
    }
    
    public async Task<Guid?> RegisterAsync(RegisterDto dto)
    {
        User usr = new User{Login = dto.Login, Password = dto.Password};

        if (await _userRepository.CreateAsync(usr))
        {
            return await _userRepository.GetUserByLoginAsync(usr.Login);
        }
        return null;
        
    }

    public async Task<bool> ExicstCheckAsync(CheckExistDto dto)
    {
        // тут что бы не делать дубль для чек экзист вместо имейла имеется ввиду login
        return await _userRepository.ExicstCheckByLoginAsync(dto.Email);
    }

    public async Task<bool> AddMail(ADDMailDto addMailDto)
    {
        return await _mailComprRepository.AddMailAsync(addMailDto.Appid, addMailDto.Email);
    }

    public async Task<AppIdDto?> GetAppId(CheckExistDto dto)
    {
        return new AppIdDto { AppId = await _userRepository.GetUserByLoginAsync(dto.Email) };
    }

}