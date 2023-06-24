using BackendSpotify.Core.Application.Configuration.Interfaces;
using BackendSpotify.Core.Application.Configuration.Services;
using BackendSpotify.Core.Application.Identity.Interfaces;
using BackendSpotify.Core.Application.Identity.Services;
using BackendSpotify.Core.Application.State.Interfaces;
using BackendSpotify.Core.Application.State.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BackendSpotify.Core.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISpotifyConfigService, SpotifyConfigService>();
        services.AddScoped<ISpotifyService, SpotifyService>();
        services.AddScoped<IStateService, StateService>();
        return services;
    }
}
