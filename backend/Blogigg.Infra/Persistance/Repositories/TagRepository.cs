using Bloggig.Domain.Entities;
using Bloggig.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bloggig.Infra.Persistance.Repositories;

public class TagRepository : ITagRepository
{
    private readonly BloggigDbContext _context;

    public TagRepository(BloggigDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Tag tag)
    {
        await _context.Tags.AddAsync(tag);
        await _context.SaveChangesAsync();
    }

    public async Task<Tag?> GetTagByName(string name)
    {
        return await _context.Tags.FirstOrDefaultAsync(x => x.Name.Contains(name));
    }
}
