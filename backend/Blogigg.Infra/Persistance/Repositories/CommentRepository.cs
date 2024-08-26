using Bloggig.Domain.Entities;
using Bloggig.Domain.Repositories;

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

    public Task DeleteAsync(Comment comment)
    {
        throw new NotImplementedException();
    }

    public Task<Post> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Comment comment)
    {
        throw new NotImplementedException();
    }
}
