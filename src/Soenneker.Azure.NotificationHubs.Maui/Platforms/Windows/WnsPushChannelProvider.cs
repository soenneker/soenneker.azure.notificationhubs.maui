#if WINDOWS
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Azure.NotificationHubs.Maui.Abstract;
using Windows.Networking.PushNotifications;

namespace Soenneker.Azure.NotificationHubs.Maui.Platforms.Windows;

/// <inheritdoc cref="IAzureNotificationHubMauiPushChannelProvider" />
internal sealed class WnsPushChannelProvider : IAzureNotificationHubMauiPushChannelProvider
{
    private readonly IAzureNotificationHubMauiPushChannelStore _store;

    public WnsPushChannelProvider(IAzureNotificationHubMauiPushChannelStore store)
    {
        _store = store;
    }

    public async ValueTask<AzureNotificationHubMauiPushChannel> GetPushChannel(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!OperatingSystem.IsWindowsVersionAtLeast(10, 0, 19041))
            throw new PlatformNotSupportedException("WNS push channel URI registration requires Windows 10 version 2004 or later.");

        PushNotificationChannel channel;

        try
        {
            channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync()
                .AsTask(cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception exception) when (exception is FileNotFoundException or MissingMethodException or NotSupportedException or TypeLoadException)
        {
            throw new PlatformNotSupportedException("WNS push channel URI registration is not available in this application context.", exception);
        }

        if (string.IsNullOrWhiteSpace(channel.Uri))
            throw new InvalidOperationException("WNS returned an empty push channel URI.");

        await _store.Set(AzureNotificationHubMauiPlatform.Wns, channel.Uri, cancellationToken).ConfigureAwait(false);

        return new AzureNotificationHubMauiPushChannel(AzureNotificationHubMauiPlatform.Wns, channel.Uri);
    }
}
#endif
