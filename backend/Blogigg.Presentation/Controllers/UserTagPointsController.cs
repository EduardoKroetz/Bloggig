using Bloggig.Application.DTOs;
using Bloggig.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bloggig.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserTagPointsController : Controller
{
    private readonly IUserTagPointsService _userTagService;
    private readonly IPostService _postService;

    public UserTagPointsController(IUserTagPointsService userTagService, IPostService postService)
    {
        _userTagService = userTagService;
        _postService = postService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateUserTagPointsAsync([FromBody] Guid postId)
    {
        var userId = Utils.Utils.GetUserIdFromClaim(User);

        var post = await _postService.GetPostById(postId);
        if (post == null)
        {
            return NotFound(ResultDto.BadResult("Post não encontrado"));
        }

        var tagIds = post.Tags.Select(t => t.Id).ToList();
        await _userTagService.AddPointsAsync(userId, tagIds);

        return Ok(ResultDto.SuccessResult("Ponto adicionado com sucesso!"));
    }
}
