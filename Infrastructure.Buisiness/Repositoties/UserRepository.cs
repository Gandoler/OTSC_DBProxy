using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace UseCases.Repositoties;

public class UserRepository: IUserRepository
{
    
    private readonly DbContext _context;

    public UserRepository(DbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> UpdateAsync(User user)
    {

        _context.Set<User>().Update(user);
        int res = await _context.SaveChangesAsync();
        return res > 0;
    }

    public async Task<bool> CreateAsync(User user)
    {
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
        if (us == null) return false;
        return true;
    }
}