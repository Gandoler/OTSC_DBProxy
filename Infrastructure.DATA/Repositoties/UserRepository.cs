using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DATA;
using Microsoft.EntityFrameworkCore;

namespace UseCases.Repositoties;

public class UserRepository: IUserRepository
{
    
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<bool> UpdateAsync(User user)
    {
        var existingUser = await _context.Set<User>().Where(f=> f.Login == user.Login).FirstOrDefaultAsync();
        if (existingUser == null)
        {
            return false; // Пользователь не найден
        }
        existingUser.Password = user.Password;
        _context.Set<User>().Update(existingUser);
        int res = await _context.SaveChangesAsync();            
        return res > 0;
    }

    public async Task<bool> CreateAsync(User user)
    {
        bool exists = await _context.Set<User>().AnyAsync(u => u.Login == user.Login);
        if (exists)
        {
            return false;
        }

        _context.Set<User>().Add(user);
        int res = await _context.SaveChangesAsync();
        return res > 0;
    }


    public async Task<bool> ForPswAndLoginCheckAsync(User user)
    {
        User? us = await _context.Set<User>().Where(f=>f.Password==user.Password && f.Login==user.Login).FirstOrDefaultAsync();
        if (us == null) return false;
        return true;
    }

    public async Task<bool> ExicstCheckByLoginAsync(string login)
    {
        User? us = await _context.Set<User>().Where(f=>f.Login==login).FirstOrDefaultAsync();
        if (us != null) return true;
        return false;
    }

    public async Task<Guid> GetUserByLoginAsync(string login)
    {
        return await _context.Set<User>().
            Where(f => f.Login == login)
            .Select(f=>f.Appid)
            .FirstOrDefaultAsync();
    }

    public async Task<string?> GetUserByIdAsync(Guid id)
    {
        return await _context.Set<User>().Where(f=>f.Appid==id).
            Select(f=>f.Login).FirstOrDefaultAsync();
    }

   
}