using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;

namespace Soenneker.Azure.NotificationHubs.Maui.Abstract;

/// <summary>
/// Checks and requests notification permission for the current platform.
/// </summary>
public interface IAzureNotificationHubMauiPermissionService
{
    /// <summary>
    /// Checks the current notification permission status.
    /// </summary>
    /// <param name="cancellationToken">A token to observe while checking permission.</param>
    /// <returns>The current permission status.</returns>
    ValueTask<PermissionStatus> CheckPermission(CancellationToken cancellationToken = default);

    /// <summary>
    /// Requests notification permission from the user.
    /// </summary>
    /// <param name="cancellationToken">A token to observe while requesting permission.</param>
    /// <returns>The resulting permission status.</returns>
    ValueTask<PermissionStatus> RequestPermission(CancellationToken cancellationToken = default);
}
