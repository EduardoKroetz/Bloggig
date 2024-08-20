namespace Bloggig.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string ProfileImageUrl { get; set; }
    public string OAuthProvider { get; set; }
    public string OAuthId { get; set; }
    public DateTime CreatedAt { get; set; }
}
