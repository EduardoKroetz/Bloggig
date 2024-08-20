using Bloggig.Domain.Entities;
using Bloggig.Infra.Persistance;
using Microsoft.AspNetCore.Mvc;

namespace Bloggig.Presentation.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : Controller
{
    private readonly BloggigDbContext _dbContext;

    public UserController(BloggigDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task CreateAsync()
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            OAuthId = "",
            OAuthProvider = "Google",
            PasswordHash = "13234234",
            ProfileImageUrl = "",
            Username = "Dudu",
            Email = "teste@gmail.com",
            CreatedAt = DateTime.Now,
        };

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        Ok(user);
    }
}
