using Soenneker.Azure.NotificationHubs.Maui.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Azure.NotificationHubs.Maui.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class AzureNotificationHubMauiUtilTests : HostedUnitTest
{
    private readonly IAzureNotificationHubMauiUtil _util;

    public AzureNotificationHubMauiUtilTests(Host host) : base(host)
    {
        _util = Resolve<IAzureNotificationHubMauiUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
