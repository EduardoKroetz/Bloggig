using Bloggig.Domain.Entities;

namespace Bloggig.Domain.Repositories;

public interface IPostRepository
{
    Task AddAsync(Post post);
    Task<IEnumerable<Post>> GetByReferencesAsync(List<string> references);
    Task<Post?> GetById(Guid id);
    Task UpdateAsync(Post post);
}
