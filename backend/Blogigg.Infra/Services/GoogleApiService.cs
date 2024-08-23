
using Bloggig.Infra.Models;
using Bloggig.Infra.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Bloggig.Infra.Services;

public class GoogleApiService : IGoogleApiService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public GoogleApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<string> ExchangeOAuthCodeForTokenAsync(string code)
    {
        var httpClient = _httpClientFactory.CreateClient();

        var clientId = _configuration["Google:ClientId"] ?? throw new Exception("Invalid google client id");
        var clientSecret = _configuration["Google:ClientSecret"] ?? throw new Exception("Invalid google client secret");
        var requestBody = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("code", code),
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("client_secret", clientSecret),
            new KeyValuePair<string, string>("redirect_uri", "/api/Auth/oauth/callback"),
            new KeyValuePair<string, string>("grant_type", "authorization_code")
        });

        var response = await httpClient.PostAsync("https://oauth2.googleapis.com/token", requestBody);
        var responseBody = await response.Content.ReadAsStringAsync();

        var json = JsonConvert.DeserializeObject<GoogleAccessToken>(responseBody);
        return json.AccessToken;
    }

    public async Task<GoogleUserInfo> GetUserInfo(string accessToken)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "https://www.googleapis.com/oauth2/v3/userinfo");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException("Não foi possível obter as informações do usuário.");
        }

        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<GoogleUserInfo>(json);
    }
}
