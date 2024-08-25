using Bloggig.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Bloggig.Application.Services.Interfaces;

public interface IAuthenticationService
{
    Task SetCookieAsync(HttpContext httpContext, User user);
}
