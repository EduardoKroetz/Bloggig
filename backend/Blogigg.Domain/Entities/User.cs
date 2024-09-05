namespace Bloggig.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string? ProfileImageUrl { get; private set; }
    public bool IsOAuthUser { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int CurrentPostsPageNumber { get; set; } = 1;

    public User(Guid id, string username, string email, string passwordHash, string? profileImageUrl, bool isOAuthUser, DateTime createdAt)
    {
        Id = id;
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        ProfileImageUrl = profileImageUrl;
        IsOAuthUser = isOAuthUser;
        CreatedAt = createdAt;
        CurrentPostsPageNumber = 1;
    }

    public void UpdateProfileImage(string newProfileImageUrl)
    {
        ProfileImageUrl = newProfileImageUrl;
    }

    public void UpdateEmail(string newEmail)
    {
        Email = newEmail;
    }

    public void UpdateUsername(string newUsername)
    {
        Username = newUsername;
    }
}
