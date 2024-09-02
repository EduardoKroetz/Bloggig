using Bloggig.Application.DTOs.Tags;
using Bloggig.Application.DTOs.Users;

namespace Bloggig.Application.DTOs.Posts;

public class GetPostDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid AuthorId { get; set; }
    public GetUserDto Author { get; set; }
    public string? ThumbnailUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<GetTag> Tags { get; set; } = new List<GetTag>();

}
