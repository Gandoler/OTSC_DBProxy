// <copyright file="IPozdrikRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Domain.Interfaces;

using Domain.Models;

public interface IPozdrikRepository
{
    Task<bool> AddIntAndPozhAsync(int idPozdr, string? interests, string? pozhelania);

    Task<(string?, string?)> SelectIntAndPozhAsync(int? idPozdr);

    Task<string?> SelectPozdrikAsync(int? idPozdr);

    Task<bool> AddPozdrAsync(int idPozdr, string? pozdrtext);

    Task<List<int>> SelectEmptyOrNullPozdrstringAsync();

    Task<int> CreatePozdrikAsync(string? interests, string? pozhelania);
}
