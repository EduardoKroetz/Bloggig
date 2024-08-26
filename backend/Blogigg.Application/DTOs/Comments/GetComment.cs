
using Bloggig.Application.DTOs.Users;
using Bloggig.Domain.Entities;

namespace Bloggig.Application.DTOs.Comments;

public class GetComment
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public Guid PostId { get; set; }
    public Guid AuthorId { get; set; }
    public GetUserDto Author { get; set; }
    public DateTime CreatedAt { get; set; }
}
