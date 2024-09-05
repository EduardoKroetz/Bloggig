using Bloggig.Application.Services.Interfaces;
using Bloggig.Domain.Entities;
using Bloggig.Domain.Repositories;

namespace Bloggig.Application.Services;

public class UserTagPointsService : IUserTagPointsService
{
    private readonly IUserTagPointsRepository _userTagPointsRepository;

    public UserTagPointsService(IUserTagPointsRepository userTagPointsRepository)
    {
        _userTagPointsRepository = userTagPointsRepository;
    }

    public async Task AddPointsAsync(Guid userId, List<Guid> TagIds)
    {
        foreach (var tagId in TagIds)
        {
            var userTagPoints = await _userTagPointsRepository.GetAsync(userId, tagId);
            if (userTagPoints == null)
            {
                var newPoint = new UserTagPoints
                {
                    UserId = userId,
                    TagId = tagId,
                    Points = 1
                };
                await _userTagPointsRepository.AddAsync(newPoint);
            }else
            {
                userTagPoints.Points++;
                await _userTagPointsRepository.UpdateAsync(userTagPoints);
            }
        }
    }
}
