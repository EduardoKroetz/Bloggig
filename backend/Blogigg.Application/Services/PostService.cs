using Bloggig.Application.DTOs.Posts;
using Bloggig.Application.DTOs.Tags;
using Bloggig.Application.DTOs.Users;
using Bloggig.Application.Services.Interfaces;
using Bloggig.Domain.Entities;
using Bloggig.Domain.Repositories;
using Bloggig.Infra.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Bloggig.Application.Services;

public class PostService : IPostService
{
    private readonly IAzureBlobStorageService _azureBlobStorageService;
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IDistributedCache _cache;

    public PostService(IAzureBlobStorageService azureBlobStorageService, IPostRepository postRepository, ICommentRepository commentRepository, IDistributedCache cache)
    {
        _azureBlobStorageService = azureBlobStorageService;
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _cache = cache;
    }

    public async Task<Post> CreatePostAsync(EditorPostDto editorPostDto, List<Tag> tags ,Guid userId)
    {
        //Como a thumbnail é opcional,
        //verificar se a imagem base 64 existe para então carregar no blob
        string thumbnailUrl = null;
        if (!string.IsNullOrEmpty(editorPostDto.Base64Thumbnail))
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
            Tags = tags,
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

    public async Task<IEnumerable<GetPostDto>> GetPostsByReference(string reference, int pageSize, int pageNumber)
    {
        //Vai separar as palavras por espaços em branco para então filtrar
        //as palavras com mais de 3 caracteres
        var keys = reference.ToLower().Split(" ").ToList();

        var posts = await _postRepository.GetByReferencesAsync(keys,pageSize, pageNumber);

        var postsIds = posts.Select(p => p.Id).ToList();
        var commentsCount = await _commentRepository.GetCommentsCountByPostIdsAsync(postsIds);

        return posts.Select(p => new GetPostDto
        {
            Id = p.Id,
            Content = p.Content,
            ThumbnailUrl = p.ThumbnailUrl,
            Title = p.Title,
            AuthorId = p.AuthorId,
            CreatedAt = p.CreatedAt,
            CommentsCount = commentsCount.TryGetValue(p.Id, out int value) ? value : 0,
            Tags = p.Tags.Select(t => new GetTag { Id = t.Id, Name = t.Name }).ToList(),
            Author = new GetUserDto
            {
                Id = p.Author.Id,
                Username = p.Author.Username,
                Email = p.Author.Email,
                ProfileImageUrl = p.Author.ProfileImageUrl
            }
        }).ToList();
    }

    public async Task UpdatePostAsync(EditorPostDto editorPostDto, List<Tag> tags, Post post)
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
        post.Tags = tags;

        await _postRepository.UpdateAsync(post);
    }

    public async Task DeletePostAsync(Post post)
    {
        //Deletar a thumbnail se ela existir
        if (!string.IsNullOrEmpty(post.ThumbnailUrl))
        {
            await _azureBlobStorageService.DeletePostThumbnailAsync(post.ThumbnailUrl);
        }

        post.Status = "deleted";
        post.ThumbnailUrl = null;
        post.UpdatedAt = DateTime.Now;

        await _postRepository.UpdateAsync(post);
    }

    public async Task<List<GetPostDto>> GetFeedPostsAsync(Guid userId ,int pageSize, int pageNumber)
    {
        int exploratoryPageSize = (int)( pageSize / 5 );
        int recommendedPageSize = pageSize - exploratoryPageSize;
        List<Post> allPosts;
        var allPostsKeyCache = "feed_posts";

        //Verificar se todos os posts estão em cache
        var cachedPosts = await _cache.GetStringAsync(allPostsKeyCache);
        if (cachedPosts != null)
        {
            allPosts = JsonConvert.DeserializeObject<List<Post>>(cachedPosts) ?? throw new System.Exception("Ocorreu um erro ao deserializar os posts");
            //Se estão em cache, filtrar eles
            allPosts = FilterFeedPosts(allPosts, userId, recommendedPageSize, exploratoryPageSize, pageNumber);
  
        }else
        {
            allPosts = await _postRepository.GetAllPostsAsync();
            var allPostsString = JsonConvert.SerializeObject(allPosts);

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
            };

            await _cache.SetStringAsync(allPostsKeyCache, allPostsString, cacheOptions);

            allPosts = FilterFeedPosts(allPosts, userId, recommendedPageSize, exploratoryPageSize, pageNumber);
        }

        //Mistura os posts
        var random = new Random();
        allPosts = allPosts.OrderBy(x => random.Next()).ToList();

        var postsIds = allPosts.Select(p => p.Id).ToList();
        var commentsCount = await _commentRepository.GetCommentsCountByPostIdsAsync(postsIds);

        return allPosts.Select(p => new GetPostDto
        {
            Id = p.Id,
            Content = p.Content,
            ThumbnailUrl = p.ThumbnailUrl,
            Title = p.Title,
            AuthorId = p.AuthorId,
            CreatedAt = p.CreatedAt,
            CommentsCount = commentsCount.TryGetValue(p.Id, out int value) ? value : 0,
            Tags = p.Tags.Select(t => new GetTag { Id = t.Id, Name = t.Name }).ToList(),
            Author = new GetUserDto 
            { 
                Id = p.Author.Id, 
                Username = p.Author.Username, 
                Email = p.Author.Email, 
                ProfileImageUrl = p.Author.ProfileImageUrl 
            }
        }).ToList();
    }
    
    private List<Post> FilterFeedPosts(List<Post> allPosts, Guid userId, int recommendedPageSize, int exploratoryPageSize, int pageNumber)
    {
        var recommendedPosts = FilterRecommendedPosts(allPosts, userId, recommendedPageSize, pageNumber);
        var exploratoryPosts = FilterExploratoryPosts(allPosts, exploratoryPageSize);
        //Concatenar posts recomendados com exploratórios(posts de descoberta)
        allPosts = [.. recommendedPosts, .. exploratoryPosts];
        return allPosts;
    }

    private List<Post> FilterRecommendedPosts(List<Post> posts, Guid userId , int pageSize, int pageNumber)
    {
        return posts
            .Select(post => new
            {
                Post = post,
                Points = post.Tags
                    .Select(tag => tag.UserTagPoints
                        .Where(utp => utp.UserId == userId)
                        .Select(utp => utp.Points)
                        .DefaultIfEmpty(0) // Se não houver pontos para o usuário, usa 0
                        .Sum()) // Soma dos pontos
                    .Sum() // Soma dos pontos das tags
            })
            .OrderByDescending(x => x.Points)
            .Skip(( pageNumber - 1 ) * pageSize)
            .Take(pageSize)
            .Select(x => x.Post)
            .ToList();
    }

    private List<Post> FilterExploratoryPosts(List<Post> posts, int pageSize)
    {
        if (posts.Count < 50)
            return [];

        Random random = new Random();

        return posts
            .OrderBy(x => random.Next())
            .Take(pageSize)
            .ToList();
    }
    public async Task<List<GetPostDto>> GetUserPostsAsync(Guid userId, int pageSize, int pageNumber)
    {
        var posts = await _postRepository.GetUserPostsAsync(userId ,pageSize, pageNumber);

        var postsIds = posts.Select(p => p.Id).ToList();
        var commentsCount = await _commentRepository.GetCommentsCountByPostIdsAsync(postsIds);

        return posts.Select(p => new GetPostDto
        {
            Id = p.Id,
            Content = p.Content,
            ThumbnailUrl = p.ThumbnailUrl,
            Title = p.Title,
            AuthorId = p.AuthorId,
            CreatedAt = p.CreatedAt,
            CommentsCount = commentsCount.TryGetValue(p.Id, out int value) ? value : 0,
            Tags = p.Tags.Select(t => new GetTag { Id = t.Id, Name = t.Name }).ToList(),
            Author = new GetUserDto
            {
                Id = p.Author.Id,
                Username = p.Author.Username,
                Email = p.Author.Email,
                ProfileImageUrl = p.Author.ProfileImageUrl
            }
        }).ToList();
    }
}
