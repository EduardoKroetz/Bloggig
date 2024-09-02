using Bloggig.Application.DTOs;
using Bloggig.Application.DTOs.Authentication;
using IAuthenticationService = Bloggig.Application.Services.Interfaces.IAuthenticationService;
using Bloggig.Presentation.Extensions;
using Microsoft.AspNetCore.Mvc;
using Bloggig.Application.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Bloggig.Presentation.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    private readonly IUserService _userService;
    private readonly IAuthenticationService _authenticationService;

    public AuthController(IUserService userService, IAuthenticationService authenticationService)
    {
        _userService = userService;
        _authenticationService = authenticationService;
    }


    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> LogoutAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Ok(ResultDto.SuccessResult(new { }, "Logout realizado com sucesso!"));
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

        //Setar o cookie na resposta
        await _authenticationService.SetCookieAsync(HttpContext, user);

        return Ok(ResultDto.SuccessResult(new { }, "Usuário registrado com sucesso!"));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        //Validar model state
        if (!ModelState.IsValid)
        {
            return BadRequest(ResultDto.BadResult(ModelState.GetFirstError()));
        }

        //Buscar o usuário pelo email
        var user = await _userService.GetUserByEmailAsync(loginDto.Email);
        if (user == null)
        {
            return BadRequest(ResultDto.BadResult("Email ou senha inválidos"));
        }

        if (user.IsOAuthUser)
        {
            return BadRequest(ResultDto.BadResult("Essa conta está vinculada a uma conta Google. Para continuar, faça login com o Google"));
        }

        //Verificar se a senha hash do usuário é igual a senha enviada
        var validPassword = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
        if (!validPassword)
        {
            return BadRequest(ResultDto.BadResult("Email ou senha inválidos"));
        }

        //Setar o cookie na resposta
        await _authenticationService.SetCookieAsync(HttpContext, user);

        return Ok(ResultDto.SuccessResult(new { }, "Usuário logado com sucesso!"));
    }


}
