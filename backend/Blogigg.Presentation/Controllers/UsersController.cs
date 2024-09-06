using Bloggig.Application.DTOs;
using Bloggig.Application.DTOs.Users;
using Bloggig.Application.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bloggig.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchUserAsync([FromQuery] string name, [FromQuery] int pageSize, [FromQuery] int pageNumber)
    {
        if (name == null)
        {
            return BadRequest(ResultDto.BadResult("Informe o nome pelo qual deseja procurar"));
        }
        var users = await _userService.GetUsersByName(name, pageSize, pageNumber);
        return Ok(ResultDto.SuccessResult(users, "Sucesso!"));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserAsync()
    {
        //Pegar o id do usuário da claim
        var userId = Utils.Utils.GetUserIdFromClaim(User);
        
        //Buscar o usuário pelo id 
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null) 
        {
            return NotFound(ResultDto.BadResult("Usuário não encontrado"));
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

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserAsync([FromRoute] Guid userId)
    {
        //Buscar o usuário pelo id 
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
        {
            return NotFound(ResultDto.BadResult("Usuário não encontrado"));
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
    public async Task<IActionResult> PutUserAsync([FromBody] UpdateProfileImageDto profileImageDto)
    {
        if (profileImageDto.Base64ProfileImage == null)
        {
            return BadRequest(ResultDto.BadResult("Informe a nova imagem de perfil"));
        }

        var userId = Utils.Utils.GetUserIdFromClaim(User);

        //Buscar o usuário
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
        {
            return NotFound(ResultDto.BadResult("Usuário não encontrado"));
        }

        await _userService.UpdateUserAsync(user);

        return Ok(ResultDto.SuccessResult(new { }, "Usuário atualizado com sucesso!"));
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteUserAsync()
    {
        var userId = Utils.Utils.GetUserIdFromClaim(User);

        //Buscar o usuário
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
        {
            return NotFound(ResultDto.BadResult("Usuário não encontrado"));
        }

        //Deletar o usuário do banco de dados
        await _userService.DeleteUserAsync(user);

        //Remover o cookie que é usado na autenticação
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Ok(ResultDto.SuccessResult(new { }, "Usuário deletado com sucesso!"));
    }

    [HttpPatch("profile-image")]
    [Authorize]
    public async Task<IActionResult> UpdateProfileImageAsync([FromBody] UpdateProfileImageDto updateUserDto)
    {
        var userId = Utils.Utils.GetUserIdFromClaim(User);

        //Buscar o usuário
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
        {
            return NotFound(ResultDto.BadResult("Usuário não encontrado"));
        }

        await _userService.UpdateProfileImage(updateUserDto, user);

        return Ok(ResultDto.SuccessResult(new { }, "Imagem de perfil atualizada com sucesso!"));
    }
} 
