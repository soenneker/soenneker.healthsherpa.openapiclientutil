using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.HealthSherpa.HttpClients.Registrars;
using Soenneker.HealthSherpa.OpenApiClientUtil.Abstract;

namespace Soenneker.HealthSherpa.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class HealthSherpaOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="HealthSherpaOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddHealthSherpaOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddHealthSherpaOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IHealthSherpaOpenApiClientUtil, HealthSherpaOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="HealthSherpaOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddHealthSherpaOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddHealthSherpaOpenApiHttpClientAsSingleton()
                .TryAddScoped<IHealthSherpaOpenApiClientUtil, HealthSherpaOpenApiClientUtil>();

        return services;
    }
}
