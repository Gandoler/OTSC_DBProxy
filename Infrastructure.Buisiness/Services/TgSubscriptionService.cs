using Domain.Interfaces;
using Domain.Interfaces.IServices;
using Domain.Models;
using Entities.Templates;

namespace UseCases.Services;

public class TgSubscriptionService: ITgSubscriptionService
{
    private readonly ITgComprRepository _tgComprRepository;

    public TgSubscriptionService(ITgComprRepository tgComprRepository)
    {
        _tgComprRepository = tgComprRepository;
    }

    public async Task<bool> SubscribeAsync(RegisterTgDto dto)
    {
        return await _tgComprRepository.AddTgAsync(dto.AppId,dto.TgId);
    }
}