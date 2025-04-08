// <copyright file="IFriendService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Domain.Interfaces.IServices;

using Domain.DTO.DTO.Friend;
using Domain.DTO.DTO.Pozdr;
using Domain.Models;
using Entities.Templates;

public interface IFriendService
{
    // с френдрепизитория
    Task<bool> AddFriendInListAsync(FriendDto friend);

    Task<bool> DeleteFriendFromListAsync(DeleteFriendDto friend);

    Task<bool> UpdateFriendInListAsync(FriendDto friend);

    Task<List<FriendDto>> SelectByAppIdAsync(AppIdDto appid);

    Task<PozdrikIdDto> GetPozdrikIdAsync(GetPozdrikQueryDto queryDto);

    Task<bool> AddPozdrikIdToFriendAsync(AddPozdrIdDto pozdrId);

    // c поздрик репозитория
    Task<bool> AddIntAndPozhAsync(AddIntAndPozhDto intAndPozh);

    Task<AddIntAndPozhDto> SelectIntAndPozhAsync(PozdrikIdDto pozdrik);

    Task<PozdrikIdDto> CreatePozdrikAsync(string? interests, string? pozhelania);

    // общ
    Task<bool> AddFriendAndWishAsync(FriendDto friendDto, AddIntAndPozhDto pozhDto);
}
