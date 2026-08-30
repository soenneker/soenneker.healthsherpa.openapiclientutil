using Soenneker.HealthSherpa.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Soenneker.HealthSherpa.OpenApiClientUtil.Abstract;

/// <summary>
/// Provides a lazily initialized HealthSherpa API client cached for the utility's lifetime.
/// </summary>
public interface IHealthSherpaOpenApiClientUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the generated client configured to call the HealthSherpa API.
    /// </summary>
    /// <param name="cancellationToken">Stops client initialization if the cached instance has not been created yet.</param>
    /// <returns>The generated client cached for this utility's lifetime.</returns>
    ValueTask<HealthSherpaOpenApiClient> Get(CancellationToken cancellationToken = default);
}
