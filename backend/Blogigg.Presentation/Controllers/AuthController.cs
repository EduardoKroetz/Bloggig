using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bloggig.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("oauth/google/login")]
    public IActionResult LoginWithGoogle()
    {
        var frontendUrl = _configuration["FrontendUrl"] ?? throw new Exception("Não foi possível obter a Url do site");

        return Challenge(new AuthenticationProperties { RedirectUri = "/" }, GoogleDefaults.AuthenticationScheme);
    }
    [Authorize]
    [HttpGet("oauth/callback")]
    public async Task<IActionResult> OAuthCallback()
    {
        var claim = User.Claims;
        return Ok();
    }
}
