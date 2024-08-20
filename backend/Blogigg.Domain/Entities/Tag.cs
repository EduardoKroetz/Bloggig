namespace Bloggig.Domain.Entities;

public class Tag
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Post> Posts { get; set; } = new List<Post>();
}