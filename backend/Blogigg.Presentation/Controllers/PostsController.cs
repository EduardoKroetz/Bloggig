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

    public PostsController(IUserService userService, IPostService postService)
    {
        _userService = userService;
        _postService = postService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostDto createPostDto)
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

        var post = await _postService.CreatePostAsync(createPostDto, user.Username, userId);

        return Ok(ResultDto.SuccessResult(new { post.Id, post.ThumbnailUrl }));
    }



}
