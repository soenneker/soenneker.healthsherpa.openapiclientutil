using Soenneker.HealthSherpa.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Soenneker.HealthSherpa.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IHealthSherpaOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Returns the configured health Sherpa OpenAPI Client used by the Health Sherpa OpenAPI Client.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested health Sherpa OpenAPI Client.</returns>
    ValueTask<HealthSherpaOpenApiClient> Get(CancellationToken cancellationToken = default);
}
