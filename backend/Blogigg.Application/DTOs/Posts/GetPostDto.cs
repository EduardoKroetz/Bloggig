namespace Bloggig.Application.DTOs.Posts;

public class GetPostDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid AuthorId { get; set; }
    public string? ThumbnailUrl { get; set; }
}
