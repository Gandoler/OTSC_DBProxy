// <copyright file="IUserRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Domain.Interfaces;

using Domain.Models;

public interface IUserRepository
{
    Task<bool> UpdateAsync(User user);

    Task<bool> CreateAsync(User user);

    Task<bool> ForPswAndLoginCheckAsync(User user);

    Task<bool> ExicstCheckByLoginAsync(string login);

    Task<Guid> GetUserByLoginAsync(string login);

    Task<string?> GetUserByIdAsync(Guid id);
}
