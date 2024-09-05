namespace Bloggig.Domain.Entities;

public class UserTagPoints
{
    public Guid UserId { get; set; }
    public Guid TagId { get; set; }
    public int Points { get; set; }
}
