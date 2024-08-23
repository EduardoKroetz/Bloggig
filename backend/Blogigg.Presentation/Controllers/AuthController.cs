using Azure;
using Bloggig.Application.DTOs;
using Bloggig.Domain.Services;
using Bloggig.Presentation.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bloggig.Presentation.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUserAsync(CreateUserDto createUser)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ResultDto.BadResult(ModelState.GetFirstError()));
        }

        //Validar se o email já não está cadastrado
        var userExists = await _userService.GetUserByEmailAsync(createUser.Email);
        if (userExists != null)
        {
            return BadRequest(ResultDto.BadResult("Esse email já está cadastrado"));
        }

        //Subir a imagem do usuário para o Azure Blob Storage e salvar o usuário no banco de dados
        var user = await _userService.AddUserAsync(createUser);

        //Cria uma lista de claims
        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, user.Username),
            new (ClaimTypes.Email, user.Email),
            new (ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        
        //Autenticar o usuário e definir o cookie
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        return Ok(ResultDto.SuccessResult(new { }, "Usuário registrado com sucesso!"));
    }
    
}
