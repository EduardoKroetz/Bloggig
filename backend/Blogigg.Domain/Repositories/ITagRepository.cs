using Bloggig.Domain.Entities;

namespace Bloggig.Domain.Repositories;

public interface ITagRepository
{
    Task<Tag?> GetTagByName(string name);
    Task AddAsync(Tag tag);
}
