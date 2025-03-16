using Domain.DTO.DTO.Pozdr;
using Domain.Interfaces;
using Domain.Interfaces.IServices;
using Domain.Models;
using Entities.Templates;
using UseCases.Repositoties;

namespace UseCases.Services;

public class TgBotService: ITgBotService
{
    private readonly ITgComprRepository _tgComprRepository;
    private readonly IFriendRepository _friendRepository;
    private readonly IPozdrikRepository _pozdrikRepository;

    public TgBotService(ITgComprRepository mailComprRepository, IFriendRepository friendRepository,
        IPozdrikRepository pozdrikRepository)
    {
        _tgComprRepository = mailComprRepository;
        _friendRepository = friendRepository;
        _pozdrikRepository = pozdrikRepository;
    }
    
    public async Task<List<FriendList>> SelectForTodayBithrdayAsync()
    {
        return await _friendRepository.SelectForTodayBithrdayAsync();
    }

    public async Task<TgIdDto> GetTgIdAsync(AppIdDto appId)
    {
        return new TgIdDto { TgId = await _tgComprRepository.GetTgId(appId.AppId) };
    }

    public async Task<string?> SelectPozdrStringAsync(PozdrikIdDto pozdrikId)
    {
        return await _pozdrikRepository.SelectPozdrikAsync(pozdrikId._pozdrikId);
    }
   
}