using Bloggig.Domain.Entities;
using Bloggig.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

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
        await _context.SaveChangesAsync();
    }

    public async Task<Post?> GetById(Guid id)
    {
        return await _context.Posts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);   
    }

    public async Task<IEnumerable<Post>> GetByReferencesAsync(List<string> keyWords, int pageSize, int pageNumber)
    {
        return await _context.Posts
            .AsNoTracking()
            .Include(p => p.Author)
            .Where(p => 
                keyWords.Any(keyWord => p.Tags.Any(
                    tag => tag.Name.ToLower().Contains(keyWord))) || 
                keyWords.Any(keyWord => 
                    p.Title.ToLower().Contains(keyWord))
            )
            .Where(p => p.Status != "deleted")
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .OrderByDescending(p => 
                keyWords.Where(keyWord => p.Title.ToLower().Contains(keyWord))
                .Max(keyWord => keyWord.Length))
            .ToListAsync();
    }

    public async Task UpdateAsync(Post post)
    {
        _context.Update(post);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Post>> GetPostsAsync(int pageSize, int pageNumber)
    {
        return await _context.Posts
            .AsNoTracking()
            .Include(p => p.Author)
            .Include(p => p.Tags)
            .Skip(( pageNumber - 1 ) * pageSize)
            .Take(pageSize)
            .Where(x => x.Status != "deleted")
            .ToListAsync();
    }

    public async Task<List<Post>> GetUserPostsAsync(Guid userId, int pageSize, int pageNumber)
    {
        return await _context.Posts
            .AsNoTracking()
            .Where(p => p.AuthorId == userId && p.Status != "deleted")
            .Include(p => p.Author)
            .Include(p => p.Tags)
            .Skip(( pageNumber - 1 ) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    }
}
