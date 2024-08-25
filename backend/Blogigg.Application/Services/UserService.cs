using Bloggig.Application.DTOs;
using Bloggig.Domain.Entities;
using Bloggig.Domain.Repositories;
using Bloggig.Infra.Services.Interfaces;

namespace Bloggig.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IAzureBlobStorageService _azureBlobStorageService;

    public UserService(IUserRepository userRepository, IAzureBlobStorageService azureBlobStorageService)
    {
        _userRepository = userRepository;
        _azureBlobStorageService = azureBlobStorageService;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetUserByEmailAsync(email);
    }

    public async Task AddUserAsync(CreateOAuthUserDto dto)
    {
        var user = new User
        (
            Guid.NewGuid(),
            dto.Username,
            dto.Email,
            "", //Usuário OAuth não tem senhas
            dto.ProfileImageUrl,
            true,
            DateTime.UtcNow
        );

        await _userRepository.AddAsync(user);
    }

    public async Task UpdateUserAsync(User user)
    {
        await _userRepository.UpdateAsync(user);
    }

    public async Task<User> AddUserAsync(CreateUserDto dto)
    {
        //Carregar a imagem de perfil do usuário no Azure Blob Storage e pegar a url disponível da imagem
        var profileImgUrl = await _azureBlobStorageService.UploadProfileImageAsync(dto.ProfileBase64Img, dto.Username);

        //Hashear senha
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        (
            Guid.NewGuid(),
            dto.Username,
            dto.Email,
            passwordHash,
            profileImgUrl,
            false,
            DateTime.UtcNow
        );

        await _userRepository.AddAsync(user);

        return user;
    }
}
