using System;
using System.Collections.Generic;

namespace Soenneker.Azure.NotificationHubs.Maui.Abstract;

/// <summary>
/// Raises notification tap events without performing navigation.
/// </summary>
public interface IAzureNotificationHubMauiTapRouter
{
    /// <summary>
    /// Occurs when a notification tap is routed.
    /// </summary>
    event EventHandler<AzureNotificationHubMauiNotificationTappedEventArgs>? NotificationTapped;

    /// <summary>
    /// Routes a notification tap to subscribers.
    /// </summary>
    /// <param name="data">The notification payload data supplied by the consuming app.</param>
    void RouteTap(IReadOnlyDictionary<string, string>? data = null);
}
