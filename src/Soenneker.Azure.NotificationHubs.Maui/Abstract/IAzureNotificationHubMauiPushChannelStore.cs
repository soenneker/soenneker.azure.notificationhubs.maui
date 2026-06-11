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
    /// <param name="platform">The platform represented by <paramref name="pushChannel"/>.</param>
    /// <param name="pushChannel">The platform push channel.</param>
    /// <param name="cancellationToken">A token to observe while saving the push channel.</param>
    ValueTask Set(AzureNotificationHubMauiPlatform platform, string pushChannel, CancellationToken cancellationToken = default);

    /// <summary>
    /// Clears the stored platform and push channel.
    /// </summary>
    /// <param name="cancellationToken">A token to observe while clearing the push channel.</param>
    ValueTask Clear(CancellationToken cancellationToken = default);
}
