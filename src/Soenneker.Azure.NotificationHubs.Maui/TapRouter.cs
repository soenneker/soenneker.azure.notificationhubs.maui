using System;
using System.Collections.Generic;
using Soenneker.Azure.NotificationHubs.Maui.Abstract;

namespace Soenneker.Azure.NotificationHubs.Maui;

internal sealed class TapRouter : IAzureNotificationHubMauiTapRouter
{
    public event EventHandler<AzureNotificationHubMauiNotificationTappedEventArgs>? NotificationTapped;

    public void RouteTap(IReadOnlyDictionary<string, string>? data = null)
    {
        var payload = data is null
            ? new Dictionary<string, string>(StringComparer.Ordinal)
            : new Dictionary<string, string>(data, StringComparer.Ordinal);

        NotificationTapped?.Invoke(this, new AzureNotificationHubMauiNotificationTappedEventArgs(payload));
    }
}
