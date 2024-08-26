
using System.ComponentModel.DataAnnotations;

namespace Bloggig.Application.DTOs.Posts;

public class EditorPostDto
{
    [Required(ErrorMessage = "Informe o título")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Informe o conteúdo")]
    public string Content { get; set; }
    public string? Base64Thumbnail { get; set; }
    public string[] Tags { get; set; } = [];
}
