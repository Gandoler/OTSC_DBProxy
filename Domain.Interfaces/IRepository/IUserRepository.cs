
using Domain.Models;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task<bool> UpdateAsync(User user);
    Task<bool> CreateAsync(User user);
    Task<bool> ExicstCheckAsync(User user);
    Task<bool> ExicstCheckByLoginAsync(User user);

}