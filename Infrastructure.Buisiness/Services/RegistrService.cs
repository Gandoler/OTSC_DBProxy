// <copyright file="RegistrService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UseCases.Services;

using AutoMapper;
using Domain.DTO.DTO.MailComp;
using Domain.Interfaces;
using Domain.Interfaces.IServices;
using Domain.Models;
using Entities.Templates;

public class RegistrService : IRegistrService
{
    private readonly IUserRepository userRepository;
    private readonly IMailComprRepository mailComprRepository;

    public RegistrService(IUserRepository userRepository, IMailComprRepository mailComprRepository)
    {
        this.userRepository = userRepository;
        this.mailComprRepository = mailComprRepository;
    }

    public async Task<Guid?> RegisterAsync(RegisterDto dto)
    {
        User usr = new User { Login = dto.Login, Password = dto.Password };

        if (await this.userRepository.CreateAsync(usr).ConfigureAwait(false))
        {
            return await this.userRepository.GetUserByLoginAsync(usr.Login).ConfigureAwait(false);
        }

        return null;
    }

    public async Task<bool> ExicstCheckAsync(CheckExistDto dto)
    {
        // тут что бы не делать дубль для чек экзист вместо имейла имеется ввиду login
        return await this.userRepository.ExicstCheckByLoginAsync(dto.Email).ConfigureAwait(false);
    }

    public async Task<bool> AddMail(ADDMailDto addMailDto)
    {
        return await this.mailComprRepository.AddMailAsync(addMailDto.Appid, addMailDto.Email).ConfigureAwait(false);
    }

    public async Task<AppIdDto?> GetAppId(CheckExistDto dto)
    {
        return new AppIdDto { AppId = await this.userRepository.GetUserByLoginAsync(dto.Email).ConfigureAwait(false) };
    }
}
