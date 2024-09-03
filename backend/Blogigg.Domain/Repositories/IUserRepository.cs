
using Bloggig.Domain.Entities;

namespace Bloggig.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<IEnumerable<User>> GetByNamesAsync(List<string> names, int pageSize, int pageNumber);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}
