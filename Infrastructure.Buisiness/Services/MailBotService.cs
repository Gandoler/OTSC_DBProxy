using Domain.DTO.DTO.Pozdr;
using Domain.Interfaces;
using Domain.Interfaces.IServices;
using Domain.Models;
using Entities.Templates;
using UseCases.Repositoties;

namespace UseCases.Services;

public class MailBotService: IMailBotService
{
    private readonly IMailComprRepository _mailComprRepository;
    private readonly IFriendRepository _friendRepository;
    private readonly IPozdrikRepository _pozdrikRepository;

    public MailBotService(IMailComprRepository mailComprRepository, IFriendRepository friendRepository,
        IPozdrikRepository pozdrikRepository)
    {
        _mailComprRepository = mailComprRepository;
        _friendRepository = friendRepository;
        _pozdrikRepository = pozdrikRepository;
    }
        
    public async Task<List<FriendList>> SelectForTodayBithrday()
    {
        return await _friendRepository.SelectForTodayBithrdayAsync();
    }

    public async Task<string?> SelectPozdStringAsync(PozdrikIdDto pozdrikId)
    {
        return await _pozdrikRepository.SelectPozdrikAsync(pozdrikId._pozdrikId);
    }

    public Task<string?> GetEmailAsync(AppIdDto appId)
    {
        return _mailComprRepository.GetMailByIdAsync(appId.AppId);
    }
}