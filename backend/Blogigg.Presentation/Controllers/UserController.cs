using Bloggig.Application.DTOs;
using Bloggig.Application.DTOs.Users;
using Bloggig.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bloggig.Presentation.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    } 

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserAsync()
    {
        //Pegar o id do usuário da claim
        var userId = new Guid
        (
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("Não foi possível obter o id do usuário, faça login novamente")
        );
        
        //Buscar o usuário pelo id 
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null) 
        {
            return NotFound("Usuário não encontrado");
        }

        //Criar o dto para retorno dos dados
        var dto = new GetUserDto
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            ProfileImageUrl = user.ProfileImageUrl,
        };

        return Ok(ResultDto.SuccessResult(dto, "Sucesso!"));
    }
}
