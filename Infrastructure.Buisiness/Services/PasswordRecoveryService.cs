// <copyright file="PasswordRecoveryService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UseCases.Services;

using Domain.Interfaces;
using Domain.Interfaces.IServices;
using Domain.Models;
using Entities.Templates;

public class PasswordRecoveryService : IPasswordRecoveryService
{
    private readonly IUserRepository userRepository;
    private readonly IMailComprRepository mailComprRepository;

    public PasswordRecoveryService(IUserRepository userRepository, IMailComprRepository mailComprRepository)
    {
        this.userRepository = userRepository;
        this.mailComprRepository = mailComprRepository;
    }

    public async Task<AppIdDto> GetIdByMailAsync(string mail)
    {
        return new AppIdDto { AppId = await this.mailComprRepository.GetIdByMailAsync(mail).ConfigureAwait(false) };
    }

    public async Task<bool> ExicstCheckByLoginAsync(string login)
    {
        return await this.userRepository.ExicstCheckByLoginAsync(login).ConfigureAwait(false);
    }

    public async Task<bool> ExistByMailAsync(string mail)
    {
        return await this.mailComprRepository.ExistByMailAsync(mail).ConfigureAwait(false);
    }

    public async Task<bool> UpdateAsync(LoginDto loginDto)
    {
        User usr = new User { Login = loginDto.Login, Password = loginDto.Password };
        return await this.userRepository.UpdateAsync(usr).ConfigureAwait(false);
    }

    public async Task<string?> GetLoginByMailAsync(string mail)
    {
        return await this.userRepository.GetUserByIdAsync(await this.mailComprRepository.GetIdByMailAsync(mail).ConfigureAwait(false)).ConfigureAwait(false);
    }
}
