using Soenneker.Azure.NotificationHubs.Maui.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Azure.NotificationHubs.Maui.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class AzureNotificationHubMauiServiceTests : HostedUnitTest
{
    private readonly IAzureNotificationHubMauiService _service;
    private readonly IAzureNotificationHubMauiTapRouter _tapRouter;

    public AzureNotificationHubMauiServiceTests(Host host) : base(host)
    {
        _service = Resolve<IAzureNotificationHubMauiService>(true);
        _tapRouter = Resolve<IAzureNotificationHubMauiTapRouter>(true);
    }

    [Test]
    public void Services_resolve()
    {
        _ = _service;
        _ = _tapRouter;
    }
}
