
using System.Net;
using BackendSpotify.Core.Application.Configuration.Interfaces;
using BackendSpotify.Core.Application.Identity.Interfaces;
using BackendSpotify.Core.Application.State.Interfaces;
using BackendSpotify.Core.Domain.Exceptions;
using BackendSpotify.Core.Domain.Models.State;
using BackendSpotify.Core.Domain.Models.Token;

namespace BackendSpotify.Core.Application.Identity.Services;

public class SpotifyService : ISpotifyService
{
    private readonly ISpotifyTokenRepository _tokenRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly ISpotifyConfigService _spotifyConfigService;
    private readonly IStateService _stateService;

    public SpotifyService(ISpotifyTokenRepository tokenRepository,
        IAccessTokenGenerator accessTokenGenerator,
        ISpotifyConfigService spotifyConfigService,
        IStateService stateService)
    {
        _tokenRepository = tokenRepository;
        _accessTokenGenerator = accessTokenGenerator;
        _spotifyConfigService = spotifyConfigService;
        _stateService = stateService;
    }

    public async Task<string> GetAuthorizationUrl()
    {
        

        string authorizationEndpoint = _spotifyConfigService.AuthorizationEndpoint;
        string scopes = _spotifyConfigService.Scopes;
        StateRequest state = await _stateService.GenerateState();
        string clientId = _spotifyConfigService.ClientId;
        string redirectUri = _spotifyConfigService.RedirectUri;

        string url = $"{authorizationEndpoint}?client_id={clientId}&response_type=code&redirect_uri={redirectUri}&scope={scopes}&state={state.State}";

        return await Task.FromResult(url);
    }

    public async Task<TokenResponse> GetAccessToken(string code, string state)
    {
        if (!(await _stateService.IsValidState(state)))
        {
            throw new ApiException("State invalid", HttpStatusCode.BadRequest);
        }
        
        TokenResponse tokenResponse = await _accessTokenGenerator.GenerateAccessToken(code);
        await _tokenRepository.SaveToken(tokenResponse);
        return tokenResponse;
    }
}

