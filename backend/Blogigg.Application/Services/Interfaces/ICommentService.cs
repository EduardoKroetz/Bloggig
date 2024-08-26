using Bloggig.Application.DTOs.Comments;
using Bloggig.Domain.Entities;

namespace Bloggig.Application.Services.Interfaces;

public interface ICommentService
{
    Task<Comment> CreateCommentAsync(CreateCommentDto createCommentDto, Guid userId);

}
