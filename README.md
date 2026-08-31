[![](https://img.shields.io/nuget/v/soenneker.healthsherpa.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.healthsherpa.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.healthsherpa.openapiclientutil/build-and-test.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.healthsherpa.openapiclientutil/actions/workflows/build-and-test.yml)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.healthsherpa.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.healthsherpa.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.healthsherpa.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.healthsherpa.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.healthsherpa.openapiclientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.healthsherpa.openapiclientutil/actions/workflows/codeql.yml)

# Soenneker.HealthSherpa.OpenApiClientUtil

Provides a lazily created HealthSherpa Kiota client over the shared HealthSherpa `HttpClient`.

## Install

```bash
dotnet add package Soenneker.HealthSherpa.OpenApiClientUtil
```

## Configuration

```json
{
  "HealthSherpa": {
    "ApiKey": "<API key>"
  }
}
```

`ApiKey` is required. The client uses `https://api.one.healthsherpa.com` and sends the key in the `x-api-key` header by default. You can override `HealthSherpa:ClientBaseUrl`, `HealthSherpa:AuthHeaderName`, or `HealthSherpa:AuthHeaderValueTemplate`; use `{token}` in the value template where the API key belongs.

## Register

```csharp
using Soenneker.HealthSherpa.OpenApiClientUtil.Registrars;

services.AddHealthSherpaOpenApiClientUtilAsScoped();
```

The scoped registration deliberately keeps `IHealthSherpaOpenApiHttpClient` singleton. Disposing a scope releases that utility's generated-client wrapper without tearing down the long-lived HTTP client used by later scopes.

Use `AddHealthSherpaOpenApiClientUtilAsSingleton()` when the generated-client wrapper should also live for the application lifetime.

## Usage

```csharp
using Soenneker.HealthSherpa.OpenApiClient;
using Soenneker.HealthSherpa.OpenApiClient.Models;
using Soenneker.HealthSherpa.OpenApiClientUtil.Abstract;

public sealed class HealthSherpaService(IHealthSherpaOpenApiClientUtil clientUtil)
{
    public async Task<PingResponse?> Ping(CancellationToken cancellationToken)
    {
        HealthSherpaOpenApiClient client = await clientUtil.Get(cancellationToken);

        return await client.V1.Ping.GetAsync(cancellationToken: cancellationToken);
    }
}
```

Repeated and concurrent `Get()` calls on the same utility instance reuse its lazily initialized generated client. Cancellation affects first-time initialization; pass the token separately to generated request methods as shown above.

Authentication is supplied by the underlying HTTP provider, so the Kiota adapter does not add a second `x-api-key` header.

Let the service container dispose the utility. Do not dispose the shared `HttpClient` obtained by the lower-level package.
