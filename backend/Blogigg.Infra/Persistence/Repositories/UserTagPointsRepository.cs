using Bloggig.Domain.Entities;
using Bloggig.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bloggig.Infra.Persistance.Repositories;

public class UserTagPointsRepository : IUserTagPointsRepository
{
    private readonly BloggigDbContext _context;

    public UserTagPointsRepository(BloggigDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(UserTagPoints userTagPoints)
    {
        await _context.UserTagPoints.AddAsync(userTagPoints);
        await _context.SaveChangesAsync();
    }

    public async Task<UserTagPoints?> GetAsync(Guid userId, Guid tagId) 
    {
        return await _context.UserTagPoints.FirstOrDefaultAsync(x => x.TagId == tagId && x.UserId == userId);
    }

    public async Task UpdateAsync(UserTagPoints userTagPoints)
    {
        _context.UserTagPoints.Update(userTagPoints);
        await _context.SaveChangesAsync();
    }
}
