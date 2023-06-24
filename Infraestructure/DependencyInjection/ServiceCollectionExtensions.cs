
using BackendSpotify.Core.Domain.Models.State;
using BackendSpotify.Core.Domain.Models.Token;
using BackendSpotify.Infraestructure.Middlewares;
using BackendSpotify.Infraestructure.Persistence;
using BackendSpotify.Infraestructure.Repositories.Identity;
using BackendSpotify.Infraestructure.Repositories.State;
using BackendSpotify.Infraestructure.Services.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackendSpotify.Infraestructure.DependencyInjection;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IAccessTokenGenerator, AccessTokenGenerator>();
        services.AddScoped<ISpotifyTokenRepository, SpotifyTokenRepository>();
        services.AddScoped<IStateRepository, StateRepository>();


        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(
                config.GetConnectionString("DefaultConnection"));
        });
        services.AddHttpClient<IAccessTokenGenerator, AccessTokenGenerator>(client =>
        {
            client.BaseAddress = new Uri(config["Spotify:TokenEndpoint"]);
        });

        services.AddScoped<MiddlewareApiException>();
        return services;
    }
}

