

using Bloggig.Domain.Entities;

namespace Bloggig.Domain.Repositories;

public interface ICommentRepository
{
    Task AddAsync(Comment comment);
    Task DeleteAsync(Comment comment);
    Task UpdateAsync(Comment comment);
    Task<Comment?> GetById(Guid id);
    Task<IEnumerable<Comment>> GetPostComments(Guid postId, int pageSize, int pageNumber);
    Task<Dictionary<Guid, int>> GetCommentsCountByPostIdsAsync(List<Guid> postIds);
}
