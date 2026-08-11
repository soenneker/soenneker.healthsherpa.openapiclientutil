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
    ValueTask<HealthSherpaOpenApiClient> Get(CancellationToken cancellationToken = default);
}
