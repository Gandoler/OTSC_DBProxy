// <copyright file="TgSubscriptionService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UseCases.Services;

using Domain.Interfaces;
using Domain.Interfaces.IServices;
using Domain.Models;
using Entities.Templates;

public class TgSubscriptionService : ITgSubscriptionService
{
    private readonly ITgComprRepository tgComprRepository;

    public TgSubscriptionService(ITgComprRepository tgComprRepository)
    {
        this.tgComprRepository = tgComprRepository;
    }

    public async Task<bool> SubscribeAsync(RegisterTgDto dto)
    {
        return await this.tgComprRepository.AddTgAsync(dto.AppId, dto.TgId).ConfigureAwait(false);
    }
}
