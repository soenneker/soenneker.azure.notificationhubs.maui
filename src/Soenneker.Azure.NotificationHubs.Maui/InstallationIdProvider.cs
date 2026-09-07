using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Soenneker.Asyncs.Initializers;
using Soenneker.Azure.NotificationHubs.Maui.Abstract;
using Soenneker.Azure.NotificationHubs.Maui.Internal;

namespace Soenneker.Azure.NotificationHubs.Maui;

/// <inheritdoc cref="IAzureNotificationHubMauiInstallationIdProvider" />
internal sealed class InstallationIdProvider : IAzureNotificationHubMauiInstallationIdProvider
{
    private readonly AsyncInitializer _initializer;
    private string? _installationId;

    public InstallationIdProvider()
    {
        _initializer = new AsyncInitializer(Initialize);
    }

    public async ValueTask<string> GetInstallationId(CancellationToken cancellationToken = default)
    {
        await _initializer.Init(cancellationToken).ConfigureAwait(false);
        return _installationId!;
    }

    private async ValueTask Initialize(CancellationToken cancellationToken)
    {
        string? installationId = await SecureStorage.Default.GetAsync(AzureNotificationHubMauiSecureStorage.InstallationIdKey).ConfigureAwait(false);

        if (string.IsNullOrWhiteSpace(installationId))
        {
            installationId = Guid.NewGuid().ToString("N");
            await SecureStorage.Default.SetAsync(AzureNotificationHubMauiSecureStorage.InstallationIdKey, installationId).ConfigureAwait(false);
        }

        _installationId = installationId;
    }
}
