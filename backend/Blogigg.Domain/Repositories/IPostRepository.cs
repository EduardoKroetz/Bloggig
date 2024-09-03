using Bloggig.Domain.Entities;

namespace Bloggig.Domain.Repositories;

public interface IPostRepository
{
    Task AddAsync(Post post);
    Task<IEnumerable<Post>> GetByReferencesAsync(List<string> references, int pageSize, int pageNumber);
    Task<Post?> GetById(Guid id);
    Task UpdateAsync(Post post);
    Task<List<Post>> GetPostsAsync(int pageSize, int pageNumber);
    Task<List<Post>> GetUserPostsAsync(Guid userId, int pageSize, int pageNumber);
}
