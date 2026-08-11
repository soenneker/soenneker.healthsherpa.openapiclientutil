using Soenneker.HealthSherpa.OpenApiClientUtil.Abstract;
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
}
