[![](https://img.shields.io/nuget/v/soenneker.healthsherpa.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.healthsherpa.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.healthsherpa.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.healthsherpa.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.healthsherpa.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.healthsherpa.openapiclientutil/)

# Soenneker.HealthSherpa.OpenApiClientUtil

Exposes a cached OpenAPI client instance.

## Install

```bash
dotnet add package Soenneker.HealthSherpa.OpenApiClientUtil
```

## Quick start

```csharp
using Soenneker.HealthSherpa.OpenApiClientUtil.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddHealthSherpaOpenApiClientUtilAsSingleton();
```

Adds `HealthSherpaOpenApiClientUtil` as a singleton service.

## What you get

- `IHealthSherpaOpenApiClientUtil` — Exposes a cached OpenAPI client instance.
- `HealthSherpaOpenApiClientUtilRegistrar` — Registers the OpenAPI client utility for dependency injection.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `HealthSherpaOpenApiClientUtilRegistrar.AddHealthSherpaOpenApiClientUtilAsSingleton(services)` | Adds `HealthSherpaOpenApiClientUtil` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `HealthSherpaOpenApiClientUtilRegistrar.AddHealthSherpaOpenApiClientUtilAsScoped(services)` | Adds `HealthSherpaOpenApiClientUtil` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Reuse the registered client instead of constructing one per operation.
- Dispose instances you own when their scope ends so held resources can be released.
