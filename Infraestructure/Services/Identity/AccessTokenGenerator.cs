using System.Net;
using System.Text;
using BackendSpotify.Core.Application.Configuration.Interfaces;
using BackendSpotify.Core.Domain.Exceptions;
using BackendSpotify.Core.Domain.Models.Token;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BackendSpotify.Infraestructure.Services.Identity;

public class AccessTokenGenerator : IAccessTokenGenerator
{
    private readonly HttpClient _httpClient;
    private readonly ISpotifyConfigService _spotifyConfigService;

    public AccessTokenGenerator(HttpClient httpClient, ISpotifyConfigService spotifyConfigService)
    {
        _httpClient = httpClient;
        _spotifyConfigService = spotifyConfigService;
    }

    public async Task<TokenResponse> GenerateAccessToken(String Code)
    {
        
        var response = await _httpClient.PostAsync(_spotifyConfigService.TokenEndpoint,
            BuildPostData(Code));

        if (!response.IsSuccessStatusCode) {
            throw new ApiException("Failed getting Token from Spotify", HttpStatusCode.FailedDependency);
        }

        var textResponse = await response.Content.ReadAsStringAsync();

        var serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        return JsonConvert.DeserializeObject<TokenResponse>(textResponse, serializerSettings);
    }

    private StringContent BuildPostData(String code) {
        var clientId = _spotifyConfigService.ClientId;
        var clientSecret = _spotifyConfigService.ClientSecret;
        var redirectUri = _spotifyConfigService.RedirectUri;

        return new StringContent(
            $"client_id={clientId}&client_secret={clientSecret}&grant_type=authorization_code&code={code}&redirect_uri={redirectUri}"
            , Encoding.UTF8, "application/x-www-form-urlencoded");
    }
}

