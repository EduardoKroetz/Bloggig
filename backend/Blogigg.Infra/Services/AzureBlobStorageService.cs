
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
    private readonly string _blobImageContainerName;
    private readonly string _blobFileServiceUrl;

    public AzureBlobStorageService(IConfiguration configuration)
    {
        _configuration = configuration;
        _blobFileServiceUrl = _configuration["Azure:BlobFileServiceUrl"] ?? throw new Exception("Invalid blob file service url");
        _blobImageContainerName = _configuration["Azure:BlobImagesContainerName"] ?? throw new Exception("Invalid blob images container name");
        var blobConnectionString = _configuration["Azure:BlobStorageConnectionString"] ?? throw new Exception("Invalid blob storage connection string");
        _blobServiceClient = new BlobServiceClient(blobConnectionString);

    }

    public async Task<string> UploadProfileImageAsync(string Base64Img, string username)
    {
        //Busca o container de imagens
        var containerClient = _blobServiceClient.GetBlobContainerClient(_blobImageContainerName);

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
            .Append(_blobImageContainerName)
            .Append("/")
            .Append(blobName);

        return imgStrBuilder.ToString();
    }
}
