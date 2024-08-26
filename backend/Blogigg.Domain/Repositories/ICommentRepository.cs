

using Bloggig.Domain.Entities;

namespace Bloggig.Domain.Repositories;

public interface ICommentRepository
{
    Task AddAsync(Comment comment);
    Task DeleteAsync(Comment comment);
    Task UpdateAsync(Comment comment);
    Task<Post> GetById(Guid id);
}
