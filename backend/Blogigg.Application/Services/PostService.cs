using Bloggig.Application.DTOs.Posts;
using Bloggig.Application.Services.Interfaces;
using Bloggig.Domain.Entities;
using Bloggig.Domain.Repositories;
using Bloggig.Infra.Services.Interfaces;

namespace Bloggig.Application.Services;

public class PostService : IPostService
{
    private readonly IAzureBlobStorageService _azureBlobStorageService;
    private readonly IPostRepository _postRepository;

    public PostService(IAzureBlobStorageService azureBlobStorageService, IPostRepository postRepository)
    {
        _azureBlobStorageService = azureBlobStorageService;
        _postRepository = postRepository;
    }

    public async Task<Post> CreatePostAsync(CreatePostDto createPostDto, string username ,Guid userId)
    {
        var thumbnailUrl = await _azureBlobStorageService.UploadPostThumbnailAsync(createPostDto.Base64Thumbnail, username, createPostDto.Title);

        var post = new Post
        {
            Id = Guid.NewGuid(),
            AuthorId = userId,
            Title = createPostDto.Title,
            Content = createPostDto.Content,
            ThumbnailUrl = thumbnailUrl,
            Status = "created",
            Tags = [],
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };

        await _postRepository.AddAsync(post);

        return post;
    }

    public async Task<IEnumerable<GetPostDto>> GetPostsByReference(string reference)
    {
        var keys = reference.Split(" ").Where(x => x.Length > 3).ToList();

        var posts = await _postRepository.GetByReferencesAsync(keys);

        return posts.Select(x => new GetPostDto
        {
            Id = x.Id,
            AuthorId = x.AuthorId,
            Content = x.Content,
            ThumbnailUrl = x.ThumbnailUrl,
            Title = x.Title
        }).ToList();
    }
}
