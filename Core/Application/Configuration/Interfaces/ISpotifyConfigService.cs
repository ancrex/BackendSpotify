namespace BackendSpotify.Core.Application.Configuration.Interfaces;
public interface ISpotifyConfigService
{
    string ClientId { get; }
    string ClientSecret { get; }
    string RedirectUri { get; }
    string TokenEndpoint { get; }
    string AuthorizationEndpoint { get; }
    string Scopes { get; }
    int TtlState { get; }
}