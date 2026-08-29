using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Azure.NotificationHubs.Maui.Abstract;

/// <summary>
/// Sends installation lifecycle changes to an app-owned backend.
/// </summary>
public interface IAzureNotificationHubMauiInstallationRegistrar
{
    /// <summary>
    /// Registers or updates an installation in the app-owned backend.
    /// </summary>
    /// <param name="installation">Installation for the register operation.</param>
    /// <param name="cancellationToken">A token to observe while registering.</param>
    /// <returns>A task that completes when callback registration is finished.</returns>
    ValueTask Register(AzureNotificationHubMauiInstallation installation, CancellationToken cancellationToken = default);

    /// <summary>
    /// Unregisters an installation in the app-owned backend.
    /// </summary>
    /// <param name="installationId">The stable installation identifier to unregister.</param>
    /// <param name="cancellationToken">A token to observe while unregistering.</param>
    /// <returns>A task that completes when the unregister operation is complete.</returns>
    ValueTask Unregister(string installationId, CancellationToken cancellationToken = default);
}
