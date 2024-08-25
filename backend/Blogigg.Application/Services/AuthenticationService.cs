using Bloggig.Domain.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using IAuthenticationService = Bloggig.Application.Services.Interfaces.IAuthenticationService;

namespace Bloggig.Application.Services;

public class AuthenticationService : IAuthenticationService
{
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

        //Autenticar o usuário e definir o cookie
        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
    }
}
