using Domain.Interfaces;
using Domain.Interfaces.IServices;
using Domain.Models;
using Entities.Templates;

namespace UseCases.Services;

public class RegistrService:IRegistrService
{
    private readonly IUserRepository _userRepository;

    public RegistrService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<bool> RegisterAsync(RegisterDto dto)
    {
        User usr = new User{Login = dto.Login, Password = dto.Password};
        return await _userRepository.CreateAsync(usr);
    }

    public async Task<bool> ExicstCheckAsync(CheckExistDto dto)
    {
        // тут что бы не делать дубль для чек экзист вместо имейла имеется ввиду login
        return await _userRepository.ExicstCheckByLoginAsync(dto.Email);
    }
}