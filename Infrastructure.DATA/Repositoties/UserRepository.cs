// <copyright file="UserRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UseCases.Repositoties;

using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DATA;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext context;

    public UserRepository(ApplicationContext context)
    {
        this.context = context;
    }

    public async Task<bool> UpdateAsync(User user)
    {
        var existingUser = await this.context.Set<User>().Where(f => f.Login == user.Login).FirstOrDefaultAsync().ConfigureAwait(false);
        if (existingUser == null)
        {
            return false; // Пользователь не найден
        }

        existingUser.Password = user.Password;
        this.context.Set<User>().Update(existingUser);
        int res = await this.context.SaveChangesAsync().ConfigureAwait(false);
        return res > 0;
    }

    public async Task<bool> CreateAsync(User user)
    {
        bool exists = await this.context.Set<User>().AnyAsync(u => u.Login == user.Login).ConfigureAwait(false);
        if (exists)
        {
            return false;
        }

        this.context.Set<User>().Add(user);
        int res = await this.context.SaveChangesAsync().ConfigureAwait(false);
        return res > 0;
    }

    public async Task<bool> ForPswAndLoginCheckAsync(User user)
    {
        User? us = await this.context.Set<User>().Where(f => f.Password == user.Password && f.Login == user.Login).FirstOrDefaultAsync().ConfigureAwait(false);
        if (us == null)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> ExicstCheckByLoginAsync(string login)
    {
        User? us = await this.context.Set<User>().Where(f => f.Login == login).FirstOrDefaultAsync().ConfigureAwait(false);
        if (us != null)
        {
            return true;
        }

        return false;
    }

    public async Task<Guid> GetUserByLoginAsync(string login)
    {
        return await this.context.Set<User>().
            Where(f => f.Login == login)
            .Select(f => f.Appid)
            .FirstOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task<string?> GetUserByIdAsync(Guid id)
    {
        return await this.context.Set<User>().Where(f => f.Appid == id).
            Select(f => f.Login).FirstOrDefaultAsync().ConfigureAwait(false);
    }
}
