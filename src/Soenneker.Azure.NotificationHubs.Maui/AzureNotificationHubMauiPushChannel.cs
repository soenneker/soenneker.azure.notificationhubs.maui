using System;

namespace Soenneker.Azure.NotificationHubs.Maui;

public sealed class AzureNotificationHubMauiPushChannel
{
    public AzureNotificationHubMauiPushChannel(AzureNotificationHubMauiPlatform platform, string pushChannel)
    {
        if (string.IsNullOrWhiteSpace(pushChannel))
            throw new ArgumentException("A push channel is required.", nameof(pushChannel));

        Platform = platform;
        PushChannel = pushChannel;
    }

    public AzureNotificationHubMauiPlatform Platform { get; }

    public string PushChannel { get; }
}
