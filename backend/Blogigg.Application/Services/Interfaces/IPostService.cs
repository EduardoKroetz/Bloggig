
using Bloggig.Application.DTOs.Posts;
using Bloggig.Domain.Entities;

namespace Bloggig.Application.Services.Interfaces;

public interface IPostService
{
    Task<Post> CreatePostAsync(EditorPostDto editorPostDto, string username, Guid userId);
    Task<IEnumerable<GetPostDto>> GetPostsByReference(string reference);
    Task<Post?> GetPostById(Guid postId);
    Task UpdatePostAsync(EditorPostDto editorPostDto, Post post);
    Task DeletePostAsync(Post post);
}
