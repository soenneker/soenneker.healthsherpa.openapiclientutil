using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.ValueTask;
using Soenneker.HealthSherpa.HttpClients.Abstract;
using Soenneker.HealthSherpa.OpenApiClientUtil.Abstract;
using Soenneker.HealthSherpa.OpenApiClient;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.HealthSherpa.OpenApiClientUtil;

/// <inheritdoc cref="IHealthSherpaOpenApiClientUtil" />
public sealed class HealthSherpaOpenApiClientUtil : IHealthSherpaOpenApiClientUtil
{
    private readonly AsyncSingleton<HealthSherpaOpenApiClient> _client;

    public HealthSherpaOpenApiClientUtil(IHealthSherpaOpenApiHttpClient httpClientUtil)
    {
        _client = new AsyncSingleton<HealthSherpaOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: httpClient);

            return new HealthSherpaOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<HealthSherpaOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
