using System.ComponentModel.DataAnnotations;

namespace Bloggig.Application.DTOs.Comments;

public class CreateCommentDto
{
    [Required(ErrorMessage = "Informe o conteúdo do comentário")]
    public string Content { get; set; }
    public Guid PostId { get; set; }
}
