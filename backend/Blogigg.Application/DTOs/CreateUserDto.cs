

namespace Bloggig.Application.DTOs;

public class CreateUserDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ProfileImageUrl { get; set; }
    public bool IsOAuthUser { get; set; }
}
