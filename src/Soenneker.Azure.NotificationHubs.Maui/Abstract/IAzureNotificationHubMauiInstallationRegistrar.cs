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
    /// <param name="installation">The installation to register.</param>
    /// <param name="cancellationToken">A token to observe while registering.</param>
    ValueTask Register(AzureNotificationHubMauiInstallation installation, CancellationToken cancellationToken = default);

    /// <summary>
    /// Unregisters an installation in the app-owned backend.
    /// </summary>
    /// <param name="installationId">The stable installation identifier to unregister.</param>
    /// <param name="cancellationToken">A token to observe while unregistering.</param>
    ValueTask Unregister(string installationId, CancellationToken cancellationToken = default);
}
