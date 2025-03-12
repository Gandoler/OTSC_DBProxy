using Domain.Interfaces;
using Domain.Interfaces.IServices;
using Domain.Models;
using Entities.Templates;

namespace UseCases.Services;

public class AuthService:IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IMailComprRepository _mailComprRepository;

    public AuthService(IUserRepository userRepository, IMailComprRepository mailComprRepository)
    {
        _userRepository = userRepository;
        _mailComprRepository = mailComprRepository;
    }
    public async Task<bool> ExicstCheckAsync(LoginDto dto)
    {
        
        
    }
}