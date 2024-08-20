namespace Bloggig.Domain.Entities;

public class Post
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid AuthorId { get; set; }
    public string ThumbnailUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Status { get; set; }
    public List<Tag> Tags { get; set; } = new List<Tag>();
}