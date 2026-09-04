using System.Threading;
using System.Threading.Tasks;
using Soenneker.Azure.NotificationHubs.Maui.Abstract;
using Soenneker.Azure.NotificationHubs.Maui.Internal;

namespace Soenneker.Azure.NotificationHubs.Maui;

/// <inheritdoc cref="IAzureNotificationHubMauiPushChannelStore" />
internal sealed class PushChannelStore : IAzureNotificationHubMauiPushChannelStore
{
    public ValueTask<AzureNotificationHubMauiPushChannel?> Get(CancellationToken cancellationToken = default)
    {
        return AzureNotificationHubMauiSecureStorage.GetPushChannel(cancellationToken);
    }

    public ValueTask Set(AzureNotificationHubMauiPlatform platform, string pushChannel, CancellationToken cancellationToken = default)
    {
        return AzureNotificationHubMauiSecureStorage.SetPushChannel(platform, pushChannel, cancellationToken);
    }

    public ValueTask Clear(CancellationToken cancellationToken = default)
    {
        return AzureNotificationHubMauiSecureStorage.ClearPushChannel(cancellationToken);
    }
}
