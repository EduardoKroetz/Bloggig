using System.ComponentModel.DataAnnotations;

namespace Bloggig.Application.DTOs.Users;

public class UpdateUserDto
{
    [Required(ErrorMessage = "Informe o nome de usuário")]
    public string Username { get; set; }

    [EmailAddress(ErrorMessage = "Formato inválido do email")]
    public string Email { get; set; }
}
