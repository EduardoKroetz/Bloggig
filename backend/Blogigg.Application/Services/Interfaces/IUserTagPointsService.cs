namespace Bloggig.Application.Services.Interfaces;

public interface IUserTagPointsService
{
    Task AddPointsAsync(Guid userId, List<Guid> TagIds);
}
