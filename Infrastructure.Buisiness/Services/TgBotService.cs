// <copyright file="TgBotService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UseCases.Services;

using Domain.DTO.DTO.Friend;
using Domain.DTO.DTO.Pozdr;
using Domain.Interfaces;
using Domain.Interfaces.IServices;
using Domain.Models;
using Entities.Templates;
using UseCases.Repositoties;

public class TgBotService : ITgBotService
{
    private readonly ITgComprRepository tgComprRepository;
    private readonly IFriendRepository friendRepository;
    private readonly IPozdrikRepository pozdrikRepository;

    public TgBotService(ITgComprRepository mailComprRepository, IFriendRepository friendRepository,
        IPozdrikRepository pozdrikRepository)
    {
        this.tgComprRepository = mailComprRepository;
        this.friendRepository = friendRepository;
        this.pozdrikRepository = pozdrikRepository;
    }

    public async Task<List<FriendList>> SelectForTodayBithrdayAsync()
    {
        return await this.friendRepository.SelectForTodayBithrdayAsync().ConfigureAwait(false);
    }

    public async Task<TgIdDto> GetTgIdAsync(AppIdDto appId)
    {
        return new TgIdDto { TgId = await this.tgComprRepository.GetTgId(appId.AppId).ConfigureAwait(false) };
    }

    public async Task<string?> SelectPozdrStringAsync(PozdrikIdDto pozdrikId)
    {
        return await this.pozdrikRepository.SelectPozdrikAsync(pozdrikId.PozdrikId).ConfigureAwait(false);
    }

    public async Task<PozdrikIdDto> GetPozdrikId(FriendDto friendDto)
    {
        return new PozdrikIdDto
        { PozdrikId = await this.friendRepository.GetPozdrikIdAsync(friendDto.FriendUsername, friendDto.AppId).ConfigureAwait(false) };
    }
}
