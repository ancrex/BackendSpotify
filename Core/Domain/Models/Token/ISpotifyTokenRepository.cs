namespace BackendSpotify.Core.Domain.Models.Token;

public interface ISpotifyTokenRepository
{
    Task SaveToken(TokenResponse tokenResponse);
}
