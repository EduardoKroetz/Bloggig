namespace Bloggig.Infra.Services.Interfaces;

public interface IAzureBlobStorageService
{
    Task<string> UploadProfileImageAsync(string Base64Img, string username);
    Task<string> UploadPostThumbnailAsync(string Base64Img, string username, string postTitle);
}
