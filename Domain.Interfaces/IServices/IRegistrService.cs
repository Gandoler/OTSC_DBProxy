// <copyright file="IRegistrService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Domain.Interfaces.IServices;

using Domain.DTO.DTO.MailComp;
using Domain.Models;
using Entities.Templates;

public interface IRegistrService
{
    Task<Guid?> RegisterAsync(RegisterDto dto);

    Task<bool> ExicstCheckAsync(CheckExistDto dto);

    Task<bool> AddMail(ADDMailDto addMailDto);

    Task<AppIdDto?> GetAppId(CheckExistDto dto);
}
