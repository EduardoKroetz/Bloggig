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

    public async Task<Post> CreatePostAsync(EditorPostDto editorPostDto, string username ,Guid userId)
    {
        //Como a thumbnail é opcional,
        //verificar se a imagem base 64 existe para então carregar no blob
        string thumbnailUrl = null;
        if (editorPostDto.Base64Thumbnail != null)
        {
           thumbnailUrl = await _azureBlobStorageService.UploadPostThumbnailAsync(editorPostDto.Base64Thumbnail, editorPostDto.Title);
        }

        var post = new Post
        {
            Id = Guid.NewGuid(),
            AuthorId = userId,
            Title = editorPostDto.Title,
            Content = editorPostDto.Content,
            ThumbnailUrl = thumbnailUrl,
            Status = "created",
            Tags = [],
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };

        //Salvar no banco de dados
        await _postRepository.AddAsync(post);

        return post;
    }
    
    public async Task<Post?> GetPostById(Guid postId)
    {
        return await _postRepository.GetById(postId);
    }

    public async Task<IEnumerable<GetPostDto>> GetPostsByReference(string reference)
    {
        //Vai separar as palavras por espaços em branco para então filtrar
        //as palavras com mais de 3 caracteres
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

    public async Task UpdatePostAsync(EditorPostDto editorPostDto, Post post)
    {
        if (!string.IsNullOrEmpty(editorPostDto.Base64Thumbnail))
        {
            //Deletar a thumbnail anterior se ela existir
            if (!string.IsNullOrEmpty(post.ThumbnailUrl))
            {
                await _azureBlobStorageService.DeletePostThumbnailAsync(post.ThumbnailUrl);
            }

            //Carregar a nova imagem do post
            post.ThumbnailUrl = await _azureBlobStorageService.UploadPostThumbnailAsync(editorPostDto.Base64Thumbnail, editorPostDto.Title);
        }

        post.Title = editorPostDto.Title;
        post.Content = editorPostDto.Content;
        post.Status = "updated";
        post.UpdatedAt = DateTime.Now;

        await _postRepository.UpdateAsync(post);
    }

}
