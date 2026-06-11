#if IOS
using System;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using Soenneker.Azure.NotificationHubs.Maui.Platforms.Ios;
using UIKit;

namespace Soenneker.Azure.NotificationHubs.Maui;

public static class AzureNotificationHubsMauiUIApplicationDelegateExtensions
{
    public static ValueTask SetAzureNotificationHubsMauiApnsPushChannel(this UIApplicationDelegate appDelegate, NSData deviceToken, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(appDelegate);
        ArgumentNullException.ThrowIfNull(deviceToken);

        byte[] bytes = deviceToken.ToArray();

        if (bytes.Length == 0)
            throw new ArgumentException("The APNs device token data was empty.", nameof(deviceToken));

        string pushChannel = Convert.ToHexString(bytes).ToLowerInvariant();
        return appDelegate.SetAzureNotificationHubsMauiApnsPushChannel(pushChannel, cancellationToken);
    }

    public static ValueTask SetAzureNotificationHubsMauiApnsPushChannel(this UIApplicationDelegate appDelegate, string pushChannel, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(appDelegate);
        return IosApnsPushChannelBridge.SetPushChannel(pushChannel, cancellationToken);
    }
}
#endif
