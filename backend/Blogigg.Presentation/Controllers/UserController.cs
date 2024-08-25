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

    private Guid GetUserIdFromClaim()
    {
        return new Guid
        (
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("Não foi possível obter o id do usuário, faça login novamente")
        );
    }

    public UserController(IUserService userService)
    {
        _userService = userService;
    } 

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserAsync()
    {
        //Pegar o id do usuário da claim
        var userId = GetUserIdFromClaim();
        
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


    [HttpPut]
    [Authorize]
    public async Task<IActionResult> PutUserAsync([FromBody] UpdateUserDto updateUserDto)
    {
        var userId = GetUserIdFromClaim();

        //Buscar o usuário
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
        {
            return NotFound("Usuário não encontrado");
        }

        user.UpdateEmail(updateUserDto.Email);
        user.UpdateUsername(updateUserDto.Username);

        await _userService.UpdateUserAsync(user);

        return Ok(ResultDto.SuccessResult(new { }, "Usuário atualizado com sucesso!"));
    }
}
