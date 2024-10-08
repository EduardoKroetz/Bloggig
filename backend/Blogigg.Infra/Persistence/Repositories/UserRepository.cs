﻿
using Bloggig.Domain.Entities;
using Bloggig.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bloggig.Infra.Persistance.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BloggigDbContext _context;

    public UserRepository(BloggigDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
    }

    public async Task<IEnumerable<User>> GetByNamesAsync(List<string> names, int pageSize ,int pageNumber)
    {
        return await _context.Users
            .AsNoTracking()
            .Where(p => names.Any(name => p.Username.ToLower().Contains(name)))
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .OrderByDescending(p => names
                .Where(name => name.Contains(p.Username.ToLower()))
                .Max(name => name.Length))
            .ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public Task<User?> GetUserByIdAsync(Guid id)
    {
        return _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}
