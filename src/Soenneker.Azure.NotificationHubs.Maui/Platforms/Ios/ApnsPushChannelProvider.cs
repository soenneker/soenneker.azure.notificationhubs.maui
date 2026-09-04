#if IOS
using System;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Azure.NotificationHubs.Maui.Abstract;

namespace Soenneker.Azure.NotificationHubs.Maui.Platforms.Ios;

/// <inheritdoc cref="IAzureNotificationHubMauiPushChannelProvider" />
internal sealed class ApnsPushChannelProvider : IAzureNotificationHubMauiPushChannelProvider
{
    private readonly IAzureNotificationHubMauiPushChannelStore _store;

    public ApnsPushChannelProvider(IAzureNotificationHubMauiPushChannelStore store)
    {
        _store = store;
    }

    public async ValueTask<AzureNotificationHubMauiPushChannel> GetPushChannel(CancellationToken cancellationToken = default)
    {
        string? pushChannel = IosApnsPushChannelBridge.PushChannel;

        if (string.IsNullOrWhiteSpace(pushChannel))
        {
            AzureNotificationHubMauiPushChannel? stored = await _store.Get(cancellationToken).ConfigureAwait(false);

            if (stored?.Platform == AzureNotificationHubMauiPlatform.Apns)
                pushChannel = stored.PushChannel;
        }

        if (string.IsNullOrWhiteSpace(pushChannel))
        {
            throw new InvalidOperationException(
                "No APNs push channel has been captured. Override RegisteredForRemoteNotifications in AppDelegate and call SetAzureNotificationHubsMauiApnsPushChannel with the APNs device token data.");
        }

        await _store.Set(AzureNotificationHubMauiPlatform.Apns, pushChannel, cancellationToken).ConfigureAwait(false);

        return new AzureNotificationHubMauiPushChannel(AzureNotificationHubMauiPlatform.Apns, pushChannel);
    }
}
#endif
