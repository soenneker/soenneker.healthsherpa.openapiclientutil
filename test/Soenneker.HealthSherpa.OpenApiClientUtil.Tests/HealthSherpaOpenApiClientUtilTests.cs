using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.HealthSherpa.HttpClients.Abstract;
using Soenneker.HealthSherpa.OpenApiClientUtil.Abstract;
using Soenneker.HealthSherpa.OpenApiClientUtil.Registrars;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.HealthSherpa.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class HealthSherpaOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IHealthSherpaOpenApiClientUtil _openapiclientutil;

    public HealthSherpaOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IHealthSherpaOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }

    [Test]
    public async Task Scoped_utility_keeps_http_client_singleton()
    {
        var services = new ServiceCollection();

        services.AddHealthSherpaOpenApiClientUtilAsScoped();

        ServiceDescriptor httpClient = services.Single(descriptor => descriptor.ServiceType == typeof(IHealthSherpaOpenApiHttpClient));
        ServiceDescriptor clientUtil = services.Single(descriptor => descriptor.ServiceType == typeof(IHealthSherpaOpenApiClientUtil));

        await Assert.That(httpClient.Lifetime).IsEqualTo(ServiceLifetime.Singleton);
        await Assert.That(clientUtil.Lifetime).IsEqualTo(ServiceLifetime.Scoped);
    }
}
