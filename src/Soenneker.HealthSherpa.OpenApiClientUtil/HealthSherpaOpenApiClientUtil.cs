using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.HealthSherpa.HttpClients.Abstract;
using Soenneker.HealthSherpa.OpenApiClientUtil.Abstract;
using Soenneker.HealthSherpa.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.HealthSherpa.OpenApiClientUtil;

/// <inheritdoc cref="IHealthSherpaOpenApiClientUtil"/>
public sealed class HealthSherpaOpenApiClientUtil : IHealthSherpaOpenApiClientUtil
{
    private readonly AsyncSingleton<HealthSherpaOpenApiClient> _client;

    public HealthSherpaOpenApiClientUtil(IHealthSherpaOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<HealthSherpaOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("HealthSherpa:ApiKey");
            string authHeaderName = configuration["HealthSherpa:AuthHeaderName"] ?? "x-api-key";
            string authHeaderValueTemplate = configuration["HealthSherpa:AuthHeaderValueTemplate"] ?? "{token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerName: authHeaderName, headerValue: authHeaderValue),
                httpClient: httpClient);

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
