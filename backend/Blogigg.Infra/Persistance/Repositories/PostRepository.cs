using Bloggig.Domain.Entities;
using Bloggig.Domain.Repositories;

namespace Bloggig.Infra.Persistance.Repositories;

public class PostRepository : IPostRepository
{
    private readonly BloggigDbContext _context;

    public PostRepository(BloggigDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Post post)
    {
        await _context.Posts.AddAsync(post);
    }
}
