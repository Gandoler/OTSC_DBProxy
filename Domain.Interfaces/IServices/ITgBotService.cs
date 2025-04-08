// <copyright file="ITgBotService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Domain.Interfaces.IServices;

using Domain.DTO.DTO.Friend;
using Domain.DTO.DTO.Pozdr;
using Domain.Models;
using Entities.Templates;

public interface ITgBotService
{
    Task<List<FriendList>> SelectForTodayBithrdayAsync();

    Task<TgIdDto> GetTgIdAsync(AppIdDto appId);

    Task<string?> SelectPozdrStringAsync(PozdrikIdDto pozdrikId);

    Task<PozdrikIdDto> GetPozdrikId(FriendDto friendDto);
}
