
using Bloggig.Application.DTOs.Posts;
using Bloggig.Domain.Entities;

namespace Bloggig.Application.Services.Interfaces;

public interface IPostService
{
    Task<Post> CreatePostAsync(CreatePostDto createPostDto, string username, Guid userId);
}
