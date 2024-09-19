using Bloggig.Application.DTOs;
using Bloggig.Application.Services;
using Bloggig.Infra.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using IAuthenticationService = Bloggig.Application.Services.Interfaces.IAuthenticationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bloggig.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OAuthController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly IGoogleApiService _googleApiService;
    private readonly string _frontendUrl;
    private readonly IAuthenticationService _authenticationService;

    public OAuthController(IConfiguration configuration, IUserService userService, IGoogleApiService googleApiService, IAuthenticationService authenticationService)
    {
        _configuration = configuration;
        _userService = userService;
        _googleApiService = googleApiService;
        _frontendUrl = _configuration["FrontendUrl"] ?? throw new System.Exception("Não foi possível obter a Url do site");
        _authenticationService = authenticationService;
    }

    [Authorize]
    [HttpGet("google/internal-callback")]
    public IActionResult GoogleInternalCallback()
    {
        return Ok();
    }

    [HttpGet("google/login")]
    public IActionResult LoginWithGoogle()
    {
        var redirectUrl = Url.Action("OAuthRedirect", "OAuth", new { provider = "Google" }, protocol: "https");
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [Authorize]
    [HttpGet("redirect")]
    public async Task<IActionResult> OAuthRedirect()
    {
        try
        {
            //Obtém o token de acesso do google do usuário  
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!authenticateResult.Succeeded)
            {
                return BadRequest(ResultDto.BadResult("Falha na autenticação"));
            }

            var accessToken = authenticateResult.Properties.Items["access_token"];
            if (string.IsNullOrEmpty(accessToken))
            {
                return BadRequest(ResultDto.BadResult("Token de acesso não encontrado"));
            }

            //Solicita as informações do usuário ao google 
            var userInfo = await _googleApiService.GetUserInfo(accessToken);

            var userExists = await _userService.GetUserByEmailAsync(userInfo.Email);
            if (userExists == null)
            {
                //Cria um novo usuário
                var createUserDto = new CreateOAuthUserDto
                {
                    Username = userInfo.Name,
                    Email = userInfo.Email,
                    Password = "",  // Senha vazia para usuários OAuth
                    ProfileImageUrl = userInfo.Picture,
                };

                userExists = await _userService.AddUserAsync(createUserDto);
            }
            else
            {
                //Atualiza o usuário existente
                userExists.UpdateEmail(userInfo.Email);
                userExists.UpdateProfileImage(userInfo.Picture);

                await _userService.UpdateUserAsync(userExists);
            }

            await _authenticationService.SetCookieAsync(HttpContext, userExists);

            return Redirect(_frontendUrl);
        }
        catch
        {
            return Redirect($"{_frontendUrl}?error=erro interno do servidor");
        }
    }
}
