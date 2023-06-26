using BackendSpotify.Core.Application.Configuration.Interfaces;
using BackendSpotify.Core.Application.Identity.Services;
using BackendSpotify.Core.Application.State.Interfaces;
using BackendSpotify.Core.Domain.Models.State;
using BackendSpotify.Core.Domain.Models.Token;
using Moq;

namespace Application.Test.Identity.Service;
public class SpotifyServiceTest
{
    private readonly Mock<IAccessTokenGenerator> accessTokenGenerator = new();
    private readonly Mock<ISpotifyTokenRepository> spotifyTokenRepository = new();
    private readonly Mock<IStateService> stateService = new();
    private readonly Mock<ISpotifyConfigService> spotifyConfigService = new();

    private readonly string authorizationEndpoint = "http://spotify.count.com";
    private readonly string scopes = "scope1";
    private readonly string clientId = "1";
    private readonly string redirectUri = "http://callback";
    private readonly string state = "state";



    [Fact]
    public async Task GetAuthorizationUrl_Return_Url()
    {
        spotifyConfigService.Setup(a => a.AuthorizationEndpoint).Returns(authorizationEndpoint);
        spotifyConfigService.Setup(a => a.Scopes).Returns(scopes);
        spotifyConfigService.Setup(a => a.ClientId).Returns(clientId);
        spotifyConfigService.Setup(a => a.RedirectUri).Returns(redirectUri);

        stateService.Setup(a => a.GenerateState()).Returns(
            Task.FromResult(
                new StateRequest() {
                    State = state
                }
            )
        );

        SpotifyService spotifyService = new SpotifyService(spotifyTokenRepository.Object, accessTokenGenerator.Object,
            spotifyConfigService.Object, stateService.Object);

        

        string urlExpected = $"{authorizationEndpoint}?client_id={clientId}&response_type=code&redirect_uri={redirectUri}&scope={scopes}&state={state}";

        string urlResult = await spotifyService.GetAuthorizationUrl();

        Assert.Equal(urlExpected, urlResult);
    }
}

