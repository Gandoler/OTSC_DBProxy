// <copyright file="INeiroGenService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Domain.Interfaces.IServices;

using Domain.DTO.DTO.Pozdr;
using Domain.Models;
using Entities.Templates;

public interface INeiroGenService
{
    Task<AddIntAndPozhDto> SelectIntAndPozhAsync(PozdrikIdDto pozdrikId);

    Task<bool> AddPozdrAsync(PozdrStringDTO pozdr);

    Task<string?> GetNameByPozdrikId(PozdrikIdDto pozdrikId);

    Task<string?> GetUserNameByPozdrikId(PozdrikIdDto pozdrikId);

    Task<List<int>> SelectEmptyOrNullPozdrstringAsync();
}
