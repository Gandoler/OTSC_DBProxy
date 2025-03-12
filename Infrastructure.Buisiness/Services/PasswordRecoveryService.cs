using Domain.Interfaces;
using Domain.Interfaces.IServices;
using Domain.Models;
using Entities.Templates;

namespace UseCases.Services;

public class PasswordRecoveryService : IPasswordRecoveryService
{
    private readonly IUserRepository _userRepository;
    private readonly IMailComprRepository _mailComprRepository;

    public PasswordRecoveryService(IUserRepository userRepository, IMailComprRepository mailComprRepository)
    {
        _userRepository = userRepository;
        _mailComprRepository = mailComprRepository;
    }
    
    public async Task<AppIdDto> GetIdByMailAsync(string mail)
    {
        return new AppIdDto { AppId = await _mailComprRepository.GetIdByMailAsync(mail) };
    }//

    public async Task<bool> ExicstCheckByLoginAsync(string login)
    {
        return await _userRepository.ExicstCheckByLoginAsync(login);
    }

    public async Task<bool> UpdateAsync(LoginDto loginDto)
    {
        User usr = new User{Login = loginDto.Login, Password = loginDto.Password};
        return await _userRepository.UpdateAsync(usr);
    }
}