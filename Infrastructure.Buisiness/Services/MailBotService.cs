// <copyright file="MailBotService.cs" company="PlaceholderCompany">
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

public class MailBotService : IMailBotService
{
    private readonly IMailComprRepository mailComprRepository;
    private readonly IFriendRepository friendRepository;
    private readonly IPozdrikRepository pozdrikRepository;

    public MailBotService(IMailComprRepository mailComprRepository, IFriendRepository friendRepository,
        IPozdrikRepository pozdrikRepository)
    {
        this.mailComprRepository = mailComprRepository;
        this.friendRepository = friendRepository;
        this.pozdrikRepository = pozdrikRepository;
    }

    public async Task<List<FriendList>> SelectForTodayBithrday()
    {
        return await this.friendRepository.SelectForTodayBithrdayAsync().ConfigureAwait(false);
    }

    public async Task<string?> SelectPozdStringAsync(PozdrikIdDto pozdrikId)
    {
        return await this.pozdrikRepository.SelectPozdrikAsync(pozdrikId.PozdrikId).ConfigureAwait(false);
    }

    public Task<string?> GetEmailAsync(AppIdDto appId)
    {
        return this.mailComprRepository.GetMailByIdAsync(appId.AppId);
    }

    public async Task<PozdrikIdDto> GetPozdrikId(FriendDto friendDto)
    {
        return new PozdrikIdDto
        { PozdrikId = await this.friendRepository.GetPozdrikIdAsync(friendDto.FriendUsername, friendDto.AppId).ConfigureAwait(false) };
    }
}
