// <copyright file="FriendService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UseCases.Services;

using AutoMapper;
using Domain.DTO.DTO.Friend;
using Domain.DTO.DTO.Pozdr;
using Domain.Interfaces;
using Domain.Interfaces.IServices;
using Domain.Models;
using Entities.Templates;

public class FriendService : IFriendService
{
    private readonly IFriendRepository friendRepository;
    private readonly IPozdrikRepository pozdrikRepository;
    private readonly IMapper mapper;

    public FriendService(IFriendRepository friendRepository, IPozdrikRepository pozdrikRepository, IMapper mapper)
    {
        this.mapper = mapper;
        this.friendRepository = friendRepository;
        this.pozdrikRepository = pozdrikRepository;
    }

    public async Task<bool> AddFriendInListAsync(FriendDto friend)
    {
        FriendList friendList = new FriendList
        {
            Appid = friend.AppId,
            FriendUsername = friend.FriendUsername,
            FriendName = friend.FriendName,
            DateBirth = friend.DateBirth,
        };
        return await this.friendRepository.AddFriendInListAsync(friendList).ConfigureAwait(false);
    }

    public async Task<bool> DeleteFriendFromListAsync(DeleteFriendDto friend)
    {
        return await this.friendRepository.DeleteFriendFromListAsync(friend.AppId, friend.FriendUsername).ConfigureAwait(false);
    }

    public async Task<bool> UpdateFriendInListAsync(FriendDto friend)
    {
        FriendList friendList = new FriendList
        {
            Appid = friend.AppId,
            FriendUsername = friend.FriendUsername,
            FriendName = friend.FriendName,
            DateBirth = friend.DateBirth,
        };
        return await this.friendRepository.UpdateFriendInListAsync(friendList).ConfigureAwait(false);
    }

    public async Task<List<FriendDto>> SelectByAppIdAsync(AppIdDto appid)
    {
        var friends = await this.friendRepository.SelectByAppIdAsync(appid.AppId).ConfigureAwait(false);
        return this.mapper.Map<List<FriendDto>>(friends);
    }

    public async Task<PozdrikIdDto> GetPozdrikIdAsync(GetPozdrikQueryDto queryDto)
    {
        return new PozdrikIdDto
        { PozdrikId = await this.friendRepository.GetPozdrikIdAsync(queryDto.Username, queryDto.Appid).ConfigureAwait(false) };
    }

    // nado
    public async Task<bool> AddPozdrikIdToFriendAsync(AddPozdrIdDto pozdrId)
    {
        return await this.friendRepository.AddPozdrikIdToFriendAsync(pozdrId.PozdrikId, pozdrId.FriendUsername, pozdrId.AppId).ConfigureAwait(false);
    }

    public async Task<bool> AddIntAndPozhAsync(AddIntAndPozhDto intAndPozh)
    {
        return await this.pozdrikRepository.AddIntAndPozhAsync(intAndPozh.IdPozdr, intAndPozh.Interests, intAndPozh.Pozhelania).ConfigureAwait(false);
    }

    public async Task<AddIntAndPozhDto> SelectIntAndPozhAsync(PozdrikIdDto pozdrik)
    {
        (string?, string?) pozdr = await this.pozdrikRepository.SelectIntAndPozhAsync(pozdrik.PozdrikId).ConfigureAwait(false);
        return new AddIntAndPozhDto { Interests = pozdr.Item1, Pozhelania = pozdr.Item2 };
    }

    // nado
    public async Task<PozdrikIdDto> CreatePozdrikAsync(string? interests, string? pozhelania)
    {
        return new PozdrikIdDto { PozdrikId = await this.pozdrikRepository.CreatePozdrikAsync(interests, pozhelania).ConfigureAwait(false) };
    }

    public async Task<bool> AddFriendAndWishAsync(FriendDto friendDto, AddIntAndPozhDto pozhDto)
    {
        FriendList friendList = new FriendList
        {
            Appid = friendDto.AppId,
            FriendUsername = friendDto.FriendUsername,
            FriendName = friendDto.FriendName,
            DateBirth = friendDto.DateBirth,
        };
        bool res1 = await this.friendRepository.AddFriendInListAsync(friendList).ConfigureAwait(false);
        int res2 = await this.pozdrikRepository.CreatePozdrikAsync(pozhDto.Interests, pozhDto.Pozhelania).ConfigureAwait(false);
        bool res3 = await this.friendRepository.AddPozdrikIdToFriendAsync(res2, friendDto.FriendUsername, friendDto.AppId).ConfigureAwait(false);
        if (res1 && res3)
        {
            return true;
        }

        return false;
    }
}
