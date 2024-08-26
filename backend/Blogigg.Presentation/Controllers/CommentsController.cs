using Bloggig.Application.DTOs;
using Bloggig.Application.DTOs.Comments;
using Bloggig.Application.Services.Interfaces;
using Bloggig.Presentation.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bloggig.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly ICommentService _commentService; 

    public CommentsController(IPostService postService, ICommentService commentService)
    {
        _postService = postService;
        _commentService = commentService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateCommentAsync([FromBody] CreateCommentDto createCommentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ResultDto.BadResult(ModelState.GetFirstError()));
        }

        var userId = Utils.Utils.GetUserIdFromClaim(User);

        var post = await _postService.GetPostById(createCommentDto.PostId);
        if (post == null) 
        {
            return NotFound(ResultDto.BadResult("Post não encontrado"));
        }

        var comment = await _commentService.CreateCommentAsync(createCommentDto, userId);
        
        return Ok(ResultDto.SuccessResult(new { comment.Id }, "Comentário criado com sucesso!"));

    }

}
