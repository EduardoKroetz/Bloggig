using Bloggig.Application.DTOs;
using Bloggig.Domain.Entities;
using Bloggig.Domain.Repositories;
using Bloggig.Domain.Services;

namespace Bloggig.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetUserByEmailAsync(email);
    }

    public async Task AddUserAsync(CreateUserDto dto)
    {
        var passwordHash = dto.IsOAuthUser ? "" : BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var user = new User
        (
            Guid.NewGuid(),
            dto.Username,
            dto.Email,
            passwordHash,
            dto.ProfileImageUrl,
            dto.IsOAuthUser,
            DateTime.UtcNow
        );

        await _userRepository.AddAsync(user);
    }

    public async Task UpdateUserAsync(User user)
    {
        await _userRepository.UpdateAsync(user);
    }
}
