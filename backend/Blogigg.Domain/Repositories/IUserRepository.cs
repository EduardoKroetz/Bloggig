
using Bloggig.Domain.Entities;

namespace Bloggig.Domain.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByEmailAsync(string email);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}
