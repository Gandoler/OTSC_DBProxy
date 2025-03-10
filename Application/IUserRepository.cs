using Entities.Models;

namespace UseCases;

public interface IUserRepository
{
    
    Task<User> CreateAsync(User user);

}