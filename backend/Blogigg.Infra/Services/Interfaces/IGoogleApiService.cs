
using Bloggig.Infra.Models;

namespace Bloggig.Infra.Services.Interfaces;

public interface IGoogleApiService
{
    Task<GoogleUserInfo> GetUserInfo(string accessToken);
    Task<string> ExchangeOAuthCodeForTokenAsync(string code);
}
