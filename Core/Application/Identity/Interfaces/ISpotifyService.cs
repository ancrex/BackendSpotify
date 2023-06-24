
using BackendSpotify.Core.Domain.Models.Token;

namespace BackendSpotify.Core.Application.Identity.Interfaces;

public interface ISpotifyService
{
    Task<string> GetAuthorizationUrl();
    Task<TokenResponse> GetAccessToken(string code, string state);
}


