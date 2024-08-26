using Bloggig.Application.DTOs.Comments;
using Bloggig.Domain.Entities;

namespace Bloggig.Application.Services.Interfaces;

public interface ICommentService
{
    Task<Comment> CreateCommentAsync(CreateCommentDto createCommentDto, Guid userId);
    Task<Comment?> GetCommentById(Guid id);
    Task UpdateCommentAsync(UpdateCommentDto updateCommentDto, Comment comment);
    Task DeleteCommentAsync(Comment comment);
    Task<IEnumerable<GetComment>> GetPostComments(Guid postId, int pageSize, int pageNumber);
}
