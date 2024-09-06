using Bloggig.Application.DTOs;
using Bloggig.Application.DTOs.Users;
using Bloggig.Domain.Entities;
using System.Drawing.Printing;

namespace Bloggig.Application.Services;

public interface IUserService
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User> AddUserAsync(CreateOAuthUserDto user);
    Task<User> AddUserAsync(CreateUserDto user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
    Task<IEnumerable<GetUserDto>> GetUsersByName(string name,int pageSize, int pageNumber);
    Task UpdateProfileImage(UpdateProfileImageDto profileImageDto, User user);
}
