using System.ComponentModel.DataAnnotations;

namespace Bloggig.Application.DTOs.Comments;

public class UpdateCommentDto
{
    [Required(ErrorMessage = "Informe o conteúdo do comentário")]
    public string Content { get; set; }
}
