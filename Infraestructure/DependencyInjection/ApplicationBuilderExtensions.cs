using BackendSpotify.Infraestructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace BackendSpotify.Infraestructure.DependencyInjection;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseMiddleware<MiddlewareApiException>();
        return app;
    }
}

