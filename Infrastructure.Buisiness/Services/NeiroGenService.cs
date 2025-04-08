// <copyright file="NeiroGenService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UseCases.Services;

using Domain.DTO.DTO.Pozdr;
using Domain.Interfaces;
using Domain.Interfaces.IServices;
using Domain.Models;
using Entities.Templates;

public class NeiroGenService : INeiroGenService
{
    private readonly IPozdrikRepository pozdrikRepository;
    private readonly IFriendRepository friendRepository;

    public NeiroGenService(IPozdrikRepository pozdrikRepository, IFriendRepository friendRepository)
    {
        this.pozdrikRepository = pozdrikRepository;
        this.friendRepository = friendRepository;
    }

    public async Task<AddIntAndPozhDto> SelectIntAndPozhAsync(PozdrikIdDto pozdrikId)
    {
        (string?, string?) pozdr = await this.pozdrikRepository.SelectIntAndPozhAsync(pozdrikId.PozdrikId).ConfigureAwait(false);
        return new AddIntAndPozhDto { Interests = pozdr.Item1, Pozhelania = pozdr.Item2 };
    }

    public async Task<bool> AddPozdrAsync(PozdrStringDTO pozdr)
    {
        return await this.pozdrikRepository.AddPozdrAsync(pozdr.PozdrikId, pozdr.Pozdr).ConfigureAwait(false);
    }

    public async Task<string?> GetNameByPozdrikId(PozdrikIdDto pozdrikId)
    {
        return await this.friendRepository.GetFrienNameByPozdrikId(pozdrikId.PozdrikId).ConfigureAwait(false);
    }

    public async Task<string?> GetUserNameByPozdrikId(PozdrikIdDto pozdrikId)
    {
        return await this.friendRepository.GetFrienUserNameByPozdrikId(pozdrikId.PozdrikId).ConfigureAwait(false);
    }

    public async Task<List<int>> SelectEmptyOrNullPozdrstringAsync()
    {
        return await this.pozdrikRepository.SelectEmptyOrNullPozdrstringAsync().ConfigureAwait(false);
    }
}
