// <copyright file="IPasswordRecoveryService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Domain.Interfaces.IServices;

using Domain.Models;
using Entities.Templates;

public interface IPasswordRecoveryService
{
    Task<AppIdDto> GetIdByMailAsync(string mail);

    Task<bool> ExicstCheckByLoginAsync(string login);

    Task<bool> ExistByMailAsync(string mail);

    Task<bool> UpdateAsync(LoginDto loginDto);

    Task<string?> GetLoginByMailAsync(string mail);
}
