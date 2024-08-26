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

    public async Task<IEnumerable<Post>> GetByReferencesAsync(List<string> keyWords)
    {
        var reference = string.Join(" ", keyWords);
        return await _context.Posts
            .Where(p => 
                p.Tags.Any(tag => 
                    keyWords.Contains(tag.Name)) || 
                p.Title.Contains(reference))
            .Where(p => p.Status != "deleted")
            .ToListAsync();
    }

    public async Task UpdateAsync(Post post)
    {
        _context.Update(post);
        await _context.SaveChangesAsync();
    }
}
