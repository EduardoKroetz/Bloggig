

using Bloggig.Application.DTOs.Comments;
using Bloggig.Application.DTOs.Users;
using Bloggig.Application.Services.Interfaces;
using Bloggig.Domain.Entities;
using Bloggig.Domain.Repositories;

namespace Bloggig.Application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<Comment> CreateCommentAsync(CreateCommentDto createCommentDto, Guid userId)
    {
        var newComment = new Comment
        {
            Id = Guid.NewGuid(),
            Content = createCommentDto.Content,
            AuthorId = userId,
            PostId = createCommentDto.PostId,
            CreatedAt = DateTime.Now
        };

        await _commentRepository.AddAsync(newComment);

        return newComment;
    }

    public async Task DeleteCommentAsync(Comment comment)
    {
        await _commentRepository.DeleteAsync(comment);
    }

    public async Task<Comment?> GetCommentById(Guid id)
    {
        return await _commentRepository.GetById(id);
    }

    public async Task<IEnumerable<GetComment>> GetPostComments(Guid postId, int pageSize, int pageNumber)
    {
        var comments = await _commentRepository.GetPostComments(postId, pageSize, pageNumber);

        return comments.Select(x => new GetComment
        {
            Id = x.Id,
            AuthorId= x.AuthorId,
            Content = x.Content,
            PostId = x.PostId,
            CreatedAt = x.CreatedAt,
            Author = new GetUserDto 
            { 
                Id = x.Author.Id,
                Email = x.Author.Email,
                ProfileImageUrl = x.Author.ProfileImageUrl,
                Username = x.Author.Username
            }, 
        }).ToList();
    }

    public async Task UpdateCommentAsync(UpdateCommentDto updateCommentDto, Comment comment)
    {
        comment.Content = updateCommentDto.Content;

        await _commentRepository.UpdateAsync(comment);
    }
}
