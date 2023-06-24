using BackendSpotify.Core.Application.Configuration.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BackendSpotify.Core.Application.Configuration.Services;
public class SpotifyConfigService : ISpotifyConfigService
{
    private readonly IConfiguration _configuration;

    public SpotifyConfigService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string ClientId => _configuration["Spotify:ClientId"];

    public string ClientSecret => _configuration["Spotify:ClientSecret"];

    public string RedirectUri => _configuration["Spotify:RedirectUri"];

    public string TokenEndpoint => _configuration["Spotify:TokenEndpoint"];

    public string AuthorizationEndpoint => _configuration["Spotify:AuthorizationEndpoint"];

    public string Scopes => _configuration["Spotify:Scopes"];

    public int TtlState => int.Parse(_configuration["Spotify:TtlState"]);
}

