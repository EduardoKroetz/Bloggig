using Bloggig.Domain.Entities;
using Bloggig.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bloggig.Infra.Persistance.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly BloggigDbContext _context;

    public CommentRepository(BloggigDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Comment comment)
    {
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
    }

    public async Task<Comment?> GetById(Guid id)
    {
        return await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Comment>> GetPostComments(Guid postId, int pageSize, int pageNumber)
    {
        var skip = ( pageNumber - 1 ) * pageSize;

        return await _context.Comments
            .Include(x => x.Author)
            .Where(x => x.PostId == postId)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task UpdateAsync(Comment comment)
    {
        _context.Comments.Update(comment);
        await _context.SaveChangesAsync();
    }
}
