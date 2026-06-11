using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Azure.NotificationHubs.Maui.Abstract;

/// <summary>
/// Provides the stable local installation identifier.
/// </summary>
public interface IAzureNotificationHubMauiInstallationIdProvider
{
    /// <summary>
    /// Gets the stable installation identifier for this app installation.
    /// </summary>
    /// <param name="cancellationToken">A token to observe while retrieving the identifier.</param>
    /// <returns>The stable installation identifier.</returns>
    ValueTask<string> GetInstallationId(CancellationToken cancellationToken = default);
}
