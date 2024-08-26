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

        //Criar o comentário e salvar no banco de dados
        var comment = await _commentService.CreateCommentAsync(createCommentDto, userId);
        
        return Ok(ResultDto.SuccessResult(new { comment.Id }, "Comentário criado com sucesso!"));

    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateCommentAsync([FromBody] UpdateCommentDto updateCommentDto, [FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ResultDto.BadResult(ModelState.GetFirstError()));
        }

        var userId = Utils.Utils.GetUserIdFromClaim(User);

        //Buscar o comentário
        var comment = await _commentService.GetCommentById(id);
        if (comment == null)
        {
            return NotFound(ResultDto.BadResult("Comentário não encontrado"));
        } 
        
        //Verificar se quem está alterando o comentário foi quem criou
        if (comment.AuthorId != userId)
        {
            return BadRequest(ResultDto.BadResult("Você não possui permissão para alterar comentário de outros usuários"));
        }

        await _commentService.UpdateCommentAsync(updateCommentDto, comment);

        return Ok(ResultDto.SuccessResult(new { comment.Id }, "Comentário criado com sucesso!"));

    }

}
