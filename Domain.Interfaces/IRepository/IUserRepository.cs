
using Domain.Models;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task<User> UpdateAsync(User user);
    Task<User> CreateAsync(User user);
    Task<bool> ExicstCheckAsync(User user);
    Task<bool> ExicstCheckByLoginAsync(User user);

}