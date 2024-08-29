using Bloggig.Domain.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using IAuthenticationService = Bloggig.Application.Services.Interfaces.IAuthenticationService;
using Microsoft.Extensions.Configuration;

namespace Bloggig.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;

    public AuthenticationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SetCookieAsync(HttpContext httpContext, User user)
    {
        //Cria uma lista de claims
        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, user.Username),
            new (ClaimTypes.Email, user.Email),
            new (ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var frontendUrl = _configuration["FrontendUrl"] ?? throw new Exception("Invalid frontend Url");

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            AllowRefresh = true,
            RedirectUri = frontendUrl,
        };

        //Autenticar o usuário e definir o cookie
        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
    }
}
