
using Bloggig.Application.DTOs.Posts;
using Bloggig.Domain.Entities;
using System.Threading.Tasks;

namespace Bloggig.Application.Services.Interfaces;

public interface IPostService
{
    Task<Post> CreatePostAsync(EditorPostDto editorPostDto, List<Tag> tags, Guid userId);
    Task<IEnumerable<GetPostDto>> GetPostsByReference(string reference, int pageSize, int pageNumber);
    Task<Post?> GetPostById(Guid postId);
    Task UpdatePostAsync(EditorPostDto editorPostDto, List<Tag> tags, Post post);
    Task DeletePostAsync(Post post);
    Task<List<GetPostDto>> GetFeedPostsAsync(Guid userId, int pageSize, int pageNumber);
    Task<List<GetPostDto>> GetUserPostsAsync(Guid userId, int pageSize, int pageNumber);
}
