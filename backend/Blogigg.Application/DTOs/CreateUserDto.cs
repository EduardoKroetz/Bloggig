using System.ComponentModel.DataAnnotations;

namespace Bloggig.Application.DTOs;

public class CreateUserDto
{
    [Required(ErrorMessage = "Informe o nome de usuário")]
    public string Username { get; set; }

    [EmailAddress(ErrorMessage = "Formato inválido do email")]
    public string Email { get; set; }

    [Required]
    [MinLength(6, ErrorMessage = "Informe uma senha com no mínimo 6 caracteres")]
    public string Password { get; set; }
    public string ProfileBase64Img { get; set; }
}
