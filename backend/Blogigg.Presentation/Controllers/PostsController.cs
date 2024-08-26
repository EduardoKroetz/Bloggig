using Bloggig.Application.DTOs;
using Bloggig.Application.DTOs.Posts;
using Bloggig.Application.Services;
using Bloggig.Application.Services.Interfaces;
using Bloggig.Presentation.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bloggig.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private IUserService _userService;
    private IPostService _postService;
    private ITagService _tagService;

    public PostsController(IUserService userService, IPostService postService, ITagService tagService)
    {
        _userService = userService;
        _postService = postService;
        _tagService = tagService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreatePostAsync([FromBody] EditorPostDto editorPostDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ResultDto.BadResult(ModelState.GetFirstError()));
        }

        var userId = Utils.Utils.GetUserIdFromClaim(User);

        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
        {
            return NotFound(ResultDto.BadResult("Usuário não encontrado, faça login e tente novamente"));
        }

        //Buscar as tags e criar se elas não existirem
        var tags = await _tagService.CreateTagsIfNotExistsAsync(editorPostDto.Tags);
        
        //Criar o post
        var post = await _postService.CreatePostAsync(editorPostDto, tags, userId);

        return Ok(ResultDto.SuccessResult(new { post.Id, post.ThumbnailUrl }));
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchPostAsync([FromQuery] string reference)
    {
        var posts = await _postService.GetPostsByReference(reference);
        return Ok(ResultDto.SuccessResult(posts, "Sucesso!"));
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> UpdatePostAsync([FromBody] EditorPostDto editorPostDto, [FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ResultDto.BadResult(ModelState.GetFirstError()));
        }

        //Pegar o id do usuário da Claim
        var userId = Utils.Utils.GetUserIdFromClaim(User);

        //Buscar o post
        var post = await _postService.GetPostById(id);
        if (post == null)
        {
            return NotFound(ResultDto.BadResult("Post não encontrado"));
        }

        //Verificar se quem está tentando alterar o post é quem criou
        if (post.AuthorId != userId)
        {
            return BadRequest(ResultDto.BadResult("Você não pode editar posts de outros autores"));
        }

        //Buscar as tags e criar se elas não existirem
        var tags = await _tagService.CreateTagsIfNotExistsAsync(editorPostDto.Tags);

        await _postService.UpdatePostAsync(editorPostDto, tags, post);

        return Ok(ResultDto.SuccessResult(new { post.Id, post.ThumbnailUrl }, "Post atualizado com sucesso!"));
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> DeletePostAsync([FromRoute] Guid id)
    {
        //Pegar o id do usuário da Claim
        var userId = Utils.Utils.GetUserIdFromClaim(User);

        //Buscar o post
        var post = await _postService.GetPostById(id);
        if (post == null)
        {
            return NotFound(ResultDto.BadResult("Post não encontrado"));
        }

        if (post.Status == "deleted")
        {
            return BadRequest(ResultDto.BadResult("Esse post já foi deletado"));
        }

        //Verificar se quem está tentando deletar o post é quem criou
        if (post.AuthorId != userId)
        {
            return BadRequest(ResultDto.BadResult("Você não pode deletar posts de outros autores"));
        }

        await _postService.DeletePostAsync(post);

        return Ok(ResultDto.SuccessResult(new { post.Id }, "Post deletado com sucesso!"));
    }
}
    