using Bloggig.Domain.Entities;

namespace Bloggig.Domain.Repositories;

public interface IPostRepository
{
    Task AddAsync(Post post);
}
