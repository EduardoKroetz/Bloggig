using Bloggig.Application.DTOs;
using Bloggig.Application.DTOs.Users;
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

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetUserByEmailAsync(email);
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task<User> AddUserAsync(CreateOAuthUserDto dto)
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

        return user;
    }

    public async Task UpdateUserAsync(User user)
    {
        await _userRepository.UpdateAsync(user);
    }

    public async Task<User> AddUserAsync(CreateUserDto dto)
    {
        string profileImgUrl = null;
        if (!string.IsNullOrEmpty(dto.ProfileBase64Img))
        {
            //Carregar a imagem de perfil do usuário no Azure Blob Storage e pegar a url disponível da imagem
            profileImgUrl = await _azureBlobStorageService.UploadProfileImageAsync(dto.ProfileBase64Img, dto.Username);
        }

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

    public async Task DeleteUserAsync(User user)
    {
        await _userRepository.DeleteAsync(user);
    }

    public async Task<IEnumerable<GetUserDto>> GetUsersByName(string name, int pageSize, int pageNumber)
    {
        //Vai separar o nome por espaços em branco para então filtrar
        //por mais de 3 caracteres
        var names = name.ToLower().Split(" ").ToList();

        var users = await _userRepository.GetByNamesAsync(names, pageSize, pageNumber);

        return users.Select(u => new GetUserDto
        {
            Id = u.Id,
            Email = u.Email,
            Username = u.Username,
            ProfileImageUrl = u.ProfileImageUrl
        }).ToList();
    }
}
