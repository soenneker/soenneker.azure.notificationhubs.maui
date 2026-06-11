#if IOS
using System;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Azure.NotificationHubs.Maui.Internal;

namespace Soenneker.Azure.NotificationHubs.Maui.Platforms.Ios;

internal static class IosApnsPushChannelBridge
{
    private static string? _pushChannel;

    internal static string? PushChannel => Volatile.Read(ref _pushChannel);

    internal static async ValueTask SetPushChannel(string pushChannel, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(pushChannel))
            throw new ArgumentException("An APNs push channel is required.", nameof(pushChannel));

        Volatile.Write(ref _pushChannel, pushChannel);
        await AzureNotificationHubMauiSecureStorage.SetPushChannel(AzureNotificationHubMauiPlatform.Apns, pushChannel, cancellationToken).ConfigureAwait(false);
    }
}
#endif
