using Bloggig.Application.DTOs;
using Bloggig.Domain.Entities;

namespace Bloggig.Application.Services;

public interface IUserService
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(Guid id);
    Task AddUserAsync(CreateOAuthUserDto user);
    Task<User> AddUserAsync(CreateUserDto user);
    Task UpdateUserAsync(User user);

}
