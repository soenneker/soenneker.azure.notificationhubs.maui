using System;
using System.Collections.Generic;

namespace Soenneker.Azure.NotificationHubs.Maui;

public sealed class AzureNotificationHubMauiNotificationTappedEventArgs : EventArgs
{
    public AzureNotificationHubMauiNotificationTappedEventArgs(IReadOnlyDictionary<string, string> data)
    {
        Data = data;
        TappedAt = DateTimeOffset.UtcNow;
    }

    public IReadOnlyDictionary<string, string> Data { get; }

    public DateTimeOffset TappedAt { get; }
}
