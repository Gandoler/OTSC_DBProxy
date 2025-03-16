
using Domain.Models;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task<bool> UpdateAsync(User user);
    Task<bool> CreateAsync(User user);
    Task<bool> ForPswAndLoginCheckAsync(User user);
    Task<bool> ExicstCheckByLoginAsync(string login);
    Task<Guid> GetUserByLoginAsync(string login);


}