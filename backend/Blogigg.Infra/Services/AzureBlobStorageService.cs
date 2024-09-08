
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Bloggig.Infra.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Bloggig.Infra.Services;

public class AzureBlobStorageService : IAzureBlobStorageService
{
    private readonly IConfiguration _configuration;
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _blobFileServiceUrl;

    public AzureBlobStorageService(IConfiguration configuration)
    {
        _configuration = configuration;
        _blobFileServiceUrl = _configuration["Azure:BlobFileServiceUrl"] ?? throw new System.Exception("Invalid blob file service url");
        var blobConnectionString = _configuration["Azure:BlobStorageConnectionString"] ?? throw new System.Exception("Invalid blob storage connection string");
        _blobServiceClient = new BlobServiceClient(blobConnectionString);

    }

    public async Task<string> UploadProfileImageAsync(string Base64Img, string username)
    {
        var blobContainerName = _configuration["Azure:BlobProfileImagesContainerName"] ?? throw new System.Exception("Invalid blob profile images container name");
        //Busca o container de imagens
        var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);

        //Criar o container se ele não existe
        await containerClient.CreateIfNotExistsAsync();

        //Pegar os bytes da imagem Base64
        byte[] imageBytes = Convert.FromBase64String(Base64Img);

        //Criar um nome único para o blob
        var blobStrBuilder = new StringBuilder();
        blobStrBuilder.Append(username);
        blobStrBuilder.Append('-');
        blobStrBuilder.Append(Guid.NewGuid());

        var blobName = blobStrBuilder.ToString();
        
        //Carregar os bytes da imagem Base64 para o blob
        using (var stream = new MemoryStream(imageBytes))
        {
            var blobClient = containerClient.GetBlobClient(blobName);

            var blobHttpHeader = new BlobHttpHeaders
            {
                ContentType = "image/jpeg"
            };

            await blobClient.UploadAsync(stream, new BlobUploadOptions
            {
                HttpHeaders = blobHttpHeader
            });
        }

        //Formatar a url pública da imagem
        var imgStrBuilder = new StringBuilder()
            .Append(_blobFileServiceUrl)
            .Append(blobContainerName)
            .Append("/")
            .Append(blobName);

        return imgStrBuilder.ToString();
    }


    public async Task<string> UploadPostThumbnailAsync(string Base64Img, string postTitle)
    {
        var blobContainerName = _configuration["Azure:BlobPostsThumbnailContainerName"] ?? throw new System.Exception("Invalid blob posts thumbnail container name");
        //Busca o container de imagens
        var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);

        //Criar o container se ele não existe
        await containerClient.CreateIfNotExistsAsync();

        //Pegar os bytes da imagem Base64
        byte[] imageBytes = Convert.FromBase64String(Base64Img);

        //Criar um nome único para o blob
        var blobStrBuilder = new StringBuilder();
        blobStrBuilder.Append(postTitle.Replace(' ', '-').ToLower());
        blobStrBuilder.Append('-');
        blobStrBuilder.Append(new Random(6).Next());

        var blobName = blobStrBuilder.ToString();

        //Carregar os bytes da imagem Base64 para o blob
        using (var stream = new MemoryStream(imageBytes))
        {
            var blobClient = containerClient.GetBlobClient(blobName);

            var blobHttpHeader = new BlobHttpHeaders
            {
                ContentType = "image/jpeg"
            };

            await blobClient.UploadAsync(stream, new BlobUploadOptions
            {
                HttpHeaders = blobHttpHeader
            });
        }

        //Formatar a url pública da imagem
        var imgStrBuilder = new StringBuilder()
            .Append(_blobFileServiceUrl)
            .Append(blobContainerName)
            .Append("/")
            .Append(blobName);

        return imgStrBuilder.ToString();
    }

    public async Task DeletePostThumbnailAsync(string thumbnailUrl)
    {
        //Pega o nome do blob
        var blobName = thumbnailUrl.Split('/').Last();

        var blobContainerName = _configuration["Azure:BlobPostsThumbnailContainerName"] ?? throw new System.Exception("Invalid blob posts thumbnail container name");
        
        //Busca o container de imagens
        var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);

        //Busca o blob
        var blobClient = containerClient.GetBlobClient(blobName);

        //Deleta o blob
        await blobClient.DeleteAsync();
    }

    public async Task DeleteProfileImageAsync(string profileImgUrl)
    {
        var blobName = profileImgUrl.Split('/').Last();

        var blobContainerName = _configuration["Azure:BlobProfileImagesContainerName"] ?? throw new System.Exception("Invalid blob profile images container name");
        
        //Busca o container de imagens
        var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);

        var blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.DeleteAsync();
    }
}
