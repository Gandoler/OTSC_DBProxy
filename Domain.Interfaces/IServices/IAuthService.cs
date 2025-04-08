// <copyright file="IAuthService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Domain.Interfaces.IServices;

using Entities.Templates;

public interface IAuthService
{
    Task<bool> ExicstCheckAsync(LoginDto dto);
}
