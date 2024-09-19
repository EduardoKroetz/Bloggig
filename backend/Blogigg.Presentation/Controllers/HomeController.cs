using Bloggig.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bloggig.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : Controller
{
    private readonly IUserService _userService;

    public HomeController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpHead("status")]
    public async Task<IActionResult> StatusAsync()
    {
        await _userService.GetUsersByName("teste", 1, 1);
        return Ok("OK");
    }
}
