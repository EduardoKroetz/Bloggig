namespace Bloggig.Domain.Entities;

public class Image
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public Guid UploadedBy { get; set; }
    public Guid? AssociatedPostId { get; set; }
    public DateTime UploadedAt { get; set; }
}