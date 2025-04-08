// <copyright file="IMailBotService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Domain.Interfaces.IServices;

using Domain.DTO.DTO.Friend;
using Domain.DTO.DTO.Pozdr;
using Domain.Models;
using Entities.Templates;

public interface IMailBotService
{
    Task<List<FriendList>> SelectForTodayBithrday();

    Task<string?> SelectPozdStringAsync(PozdrikIdDto pozdrikId);

    Task<string?> GetEmailAsync(AppIdDto appId);

    Task<PozdrikIdDto> GetPozdrikId(FriendDto friendDto);
}
