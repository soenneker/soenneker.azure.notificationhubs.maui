using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;
using Soenneker.Azure.NotificationHubs.Maui.Abstract;

namespace Soenneker.Azure.NotificationHubs.Maui;

internal sealed class PermissionService : IAzureNotificationHubMauiPermissionService
{
    public async ValueTask<PermissionStatus> CheckPermission(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.PostNotifications>().ConfigureAwait(false);

        cancellationToken.ThrowIfCancellationRequested();
        return status;
    }

    public async ValueTask<PermissionStatus> RequestPermission(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        PermissionStatus status = await Permissions.RequestAsync<Permissions.PostNotifications>().ConfigureAwait(false);

        cancellationToken.ThrowIfCancellationRequested();
        return status;
    }
}
