
using Bloggig.Application.DTOs.Posts;
using Bloggig.Domain.Entities;

namespace Bloggig.Application.Services.Interfaces;

public interface IPostService
{
    Task<Post> CreatePostAsync(EditorPostDto editorPostDto, List<Tag> tags, Guid userId);
    Task<IEnumerable<GetPostDto>> GetPostsByReference(string reference);
    Task<Post?> GetPostById(Guid postId);
    Task UpdatePostAsync(EditorPostDto editorPostDto, List<Tag> tags, Post post);
    Task DeletePostAsync(Post post);
}
