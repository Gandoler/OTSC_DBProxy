// <copyright file="ITGComprRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Domain.Interfaces;

using Domain.Models;

public interface ITgComprRepository
{
    Task<bool> AddTgAsync(Guid appid, long telegramId);

    Task<Guid> GetIdByTgAsync(long tgId);

    Task<long?> GetTgId(Guid appId);
}
