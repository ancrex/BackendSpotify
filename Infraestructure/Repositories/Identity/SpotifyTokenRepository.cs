using BackendSpotify.Core.Domain.Models.Token;
using BackendSpotify.Infraestructure.Persistence;

namespace BackendSpotify.Infraestructure.Repositories.Identity;

public class SpotifyTokenRepository : ISpotifyTokenRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SpotifyTokenRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveToken(TokenResponse tokenResponse)
    {
        _dbContext.Tokens.Add(tokenResponse);
        await _dbContext.SaveChangesAsync();
    }
}

