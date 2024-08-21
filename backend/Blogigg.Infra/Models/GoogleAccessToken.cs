
using System.Text.Json.Serialization;

namespace Bloggig.Infra.Models;

public class GoogleAccessToken
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
}
