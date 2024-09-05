using Bloggig.Domain.Entities;

namespace Bloggig.Domain.Repositories;

public interface IUserTagPointsRepository
{
    Task AddAsync(UserTagPoints userTagPoint);
    Task UpdateAsync(UserTagPoints userTagPoints);
    Task<UserTagPoints?> GetAsync(Guid userId, Guid tagId);
}
