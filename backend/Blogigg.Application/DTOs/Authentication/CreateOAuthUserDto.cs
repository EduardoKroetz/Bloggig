

namespace Bloggig.Application.DTOs;

public class CreateOAuthUserDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ProfileImageUrl { get; set; }
}
