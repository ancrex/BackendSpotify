namespace BackendSpotify.Core.Domain.Models.Token;

public interface IAccessTokenGenerator
{
    Task<TokenResponse> GenerateAccessToken(String Code);
}
