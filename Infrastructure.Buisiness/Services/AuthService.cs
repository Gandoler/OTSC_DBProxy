// <copyright file="AuthService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UseCases.Services;

using Domain.Interfaces;
using Domain.Interfaces.IServices;
using Domain.Models;
using Entities.Templates;

public class AuthService : IAuthService
{
    private readonly IUserRepository userRepository;

    public AuthService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<bool> ExicstCheckAsync(LoginDto dto)
    {
        User usr = new User { Login = dto.Login, Password = dto.Password };
        return await this.userRepository.ForPswAndLoginCheckAsync(usr).ConfigureAwait(false);
    }
}
