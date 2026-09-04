using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Soenneker.Azure.NotificationHubs.Maui.Abstract;
using Soenneker.Azure.NotificationHubs.Maui.Internal;

namespace Soenneker.Azure.NotificationHubs.Maui;

internal sealed class InstallationIdProvider : IAzureNotificationHubMauiInstallationIdProvider
/// <inheritdoc cref="IAzureNotificationHubMauiInstallationIdProvider" />
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public async ValueTask<string> GetInstallationId(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string? existing = await SecureStorage.Default.GetAsync(AzureNotificationHubMauiSecureStorage.InstallationIdKey).ConfigureAwait(false);

        if (!string.IsNullOrWhiteSpace(existing))
            return existing;

        await _semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            existing = await SecureStorage.Default.GetAsync(AzureNotificationHubMauiSecureStorage.InstallationIdKey).ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(existing))
                return existing;

            string installationId = Guid.NewGuid().ToString("N");
            await SecureStorage.Default.SetAsync(AzureNotificationHubMauiSecureStorage.InstallationIdKey, installationId).ConfigureAwait(false);

            return installationId;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
