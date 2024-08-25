using System.ComponentModel.DataAnnotations;

namespace Bloggig.Application.DTOs.Authentication;

public class LoginDto
{
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Informe a senha")]
    [MinLength(6, ErrorMessage = "Informe a senha com no mínimo 6 caracteres")]
    [MaxLength(32, ErrorMessage = "A senha deve ter no máximo 32 caracteres")]
    public string Password { get; set; }
}
