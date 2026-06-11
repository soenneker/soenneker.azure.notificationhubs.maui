using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Azure.NotificationHubs.Maui.Abstract;

/// <summary>
/// Coordinates push installation lifecycle operations for the current MAUI client.
/// </summary>
public interface IAzureNotificationHubMauiService
{
    /// <summary>
    /// Initializes local state required for installation lifecycle operations.
    /// </summary>
    /// <param name="cancellationToken">A token to observe while initializing.</param>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the current installation including the latest platform push channel.
    /// </summary>
    /// <param name="cancellationToken">A token to observe while retrieving the installation.</param>
    /// <returns>The current installation.</returns>
    ValueTask<AzureNotificationHubMauiInstallation> GetCurrentInstallation(CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends the current installation to the optional app-provided registrar.
    /// </summary>
    /// <param name="cancellationToken">A token to observe while registering.</param>
    ValueTask RegisterCurrentInstallation(CancellationToken cancellationToken = default);

    /// <summary>
    /// Unregisters the current installation through the optional app-provided registrar.
    /// </summary>
    /// <param name="cancellationToken">A token to observe while unregistering.</param>
    ValueTask UnregisterCurrentInstallation(CancellationToken cancellationToken = default);
}
