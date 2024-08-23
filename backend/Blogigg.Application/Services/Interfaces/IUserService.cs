using Bloggig.Application.DTOs;
using Bloggig.Domain.Entities;

namespace Bloggig.Domain.Services;

public interface IUserService
{
    Task<User> GetUserByEmailAsync(string email);
    Task AddUserAsync(CreateOAuthUserDto user);
    Task<User> AddUserAsync(CreateUserDto user);
    Task UpdateUserAsync(User user);

}
