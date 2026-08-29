using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Azure.NotificationHubs.Maui.Abstract;

/// <summary>
/// Persists the latest platform and push channel locally.
/// </summary>
public interface IAzureNotificationHubMauiPushChannelStore
{
    /// <summary>
    /// Gets the stored platform and push channel, if one exists.
    /// </summary>
    /// <param name="cancellationToken">A token to observe while retrieving the push channel.</param>
    /// <returns>The stored push channel, or <see langword="null"/> when no channel is stored.</returns>
    ValueTask<AzureNotificationHubMauiPushChannel?> Get(CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves the latest platform and push channel.
    /// </summary>
    /// <param name="platform">Platform for the set operation.</param>
    /// <param name="pushChannel">Push Channel for the set operation.</param>
    /// <param name="cancellationToken">A token to observe while saving the push channel.</param>
    /// <returns>A task that completes when the set operation is complete.</returns>
    ValueTask Set(AzureNotificationHubMauiPlatform platform, string pushChannel, CancellationToken cancellationToken = default);

    /// <summary>
    /// Clears the stored platform and push channel.
    /// </summary>
    /// <param name="cancellationToken">A token to observe while clearing the push channel.</param>
    /// <returns>A task that completes when the Azure Notification Hub Maui Push Channel Store has been cleared.</returns>
    ValueTask Clear(CancellationToken cancellationToken = default);
}
