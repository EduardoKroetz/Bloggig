using Bloggig.Infra.Persistance;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> CreateAsync()
    {
        return Ok("Ok");
    }
}
