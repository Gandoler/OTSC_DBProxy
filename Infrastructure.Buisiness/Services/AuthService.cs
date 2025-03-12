using Domain.Interfaces;
using Domain.Interfaces.IServices;
using Domain.Models;
using Entities.Templates;

namespace UseCases.Services;

public class AuthService:IAuthService
{
    private readonly IUserRepository _userRepository;


    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;

    }
    public async Task<bool> ExicstCheckAsync(LoginDto dto)
    {
        User usr = new User{Login = dto.Login, Password = dto.Password};
        return await _userRepository.ForPswAndLoginCheckAsync(usr);

    }
}