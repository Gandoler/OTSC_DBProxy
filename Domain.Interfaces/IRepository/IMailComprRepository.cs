// <copyright file="IMailComprRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Domain.Interfaces;

using System.Net.Mail;
using Domain.Models;

public interface IMailComprRepository
{
    Task<bool> AddMailAsync(Guid appid, string mail);

    Task<Guid> GetIdByMailAsync(string mail);

    Task<string?> GetMailByIdAsync(Guid appid);

    Task<bool> ExistByMailAsync(string mail);
}
