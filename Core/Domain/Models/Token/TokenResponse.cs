namespace BackendSpotify.Core.Domain.Models.Token;

public class TokenResponse
{
    public int Id { get; set; }
    public string AccessToken { get; set; }
    public int ExpiresIn { get; set; }
    public string RefreshToken { get; set; }
    public string TokenType { get; set; }
    public string Scope { get; set; }
}

