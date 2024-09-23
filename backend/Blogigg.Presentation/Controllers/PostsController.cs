using Bloggig.Application.DTOs;
using Bloggig.Application.DTOs.Posts;
using Bloggig.Application.DTOs.Tags;
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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPostAsync([FromRoute] Guid id)
    {
        var post = await _postService.GetPostById(id);
        if (post == null) 
        {
            return NotFound(ResultDto.BadResult("Post não encontrado"));
        }

        var dto = new GetPostDto
        {
            Id = post.Id,
            AuthorId = post.Id,
            Author = null,
            Content = post.Content,
            CreatedAt = post.CreatedAt,
            ThumbnailUrl = post.ThumbnailUrl,
            Title = post.Title,
            Tags = post.Tags
                .Select(t => new GetTag 
                { 
                    Id = t.Id, 
                    Name = t.Name 
                }).ToList()
        };

        return Ok(ResultDto.SuccessResult(dto, "Sucesso!"));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetPostsAsync([FromQuery] int pageSize = 15)
    {
        var userId = Utils.Utils.GetUserIdFromClaim(User);
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
        {
            return NotFound(ResultDto.BadResult("Usuário não encontrado"));
        }

        var posts = await _postService.GetFeedPostsAsync(userId, pageSize, user.CurrentPostsPageNumber);
        if (posts.Count < pageSize)
            user.CurrentPostsPageNumber = 1;      
        else
            user.CurrentPostsPageNumber++;
        
        await _userService.UpdateUserAsync(user);

        return Ok(ResultDto.SuccessResult(posts, "Sucesso!"));
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
    public async Task<IActionResult> SearchPostAsync([FromQuery] string reference, [FromQuery] int pageSize, [FromQuery] int pageNumber)
    {
        var posts = await _postService.GetPostsByReference(reference, pageSize, pageNumber);
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

    [HttpGet("users/{userId:guid}")]
    public async Task<IActionResult> GetUserPostsAsync([FromRoute] Guid userId, [FromQuery] int pageSize, [FromQuery] int pageNumber )
    {
        //Buscar os posts do usuário
        var posts = await _postService.GetUserPostsAsync(userId, pageSize, pageNumber);
        return Ok(ResultDto.SuccessResult(posts, "Sucesso!"));
    }
}
    