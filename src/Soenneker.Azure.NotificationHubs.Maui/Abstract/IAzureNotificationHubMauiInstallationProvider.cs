using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Azure.NotificationHubs.Maui.Abstract;

/// <summary>
/// Creates an installation model for the current device and app.
/// </summary>
public interface IAzureNotificationHubMauiInstallationProvider
{
    /// <summary>
    /// Gets the current installation including the latest platform push channel.
    /// </summary>
    /// <param name="cancellationToken">A token to observe while creating the installation.</param>
    /// <returns>The current installation.</returns>
    ValueTask<AzureNotificationHubMauiInstallation> GetCurrentInstallation(CancellationToken cancellationToken = default);
}
