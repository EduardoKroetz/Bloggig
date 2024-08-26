﻿

using Bloggig.Application.DTOs.Comments;
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
}
