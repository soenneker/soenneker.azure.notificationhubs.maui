using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Azure.NotificationHubs.Maui.Abstract;

/// <summary>
/// Retrieves the platform push channel for the current device.
/// </summary>
public interface IAzureNotificationHubMauiPushChannelProvider
{
    /// <summary>
    /// Gets the current platform push channel.
    /// </summary>
    /// <param name="cancellationToken">A token to observe while retrieving the push channel.</param>
    /// <returns>The current platform push channel.</returns>
    ValueTask<AzureNotificationHubMauiPushChannel> GetPushChannel(CancellationToken cancellationToken = default);
}
