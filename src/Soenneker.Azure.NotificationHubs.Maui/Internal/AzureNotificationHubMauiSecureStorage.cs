using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace Soenneker.Azure.NotificationHubs.Maui.Internal;

internal static class AzureNotificationHubMauiSecureStorage
{
    internal const string InstallationIdKey = "Soenneker.Azure.NotificationHubs.Maui.InstallationId";
    private const string PushChannelPlatformKey = "Soenneker.Azure.NotificationHubs.Maui.PushChannel.Platform";
    private const string PushChannelKey = "Soenneker.Azure.NotificationHubs.Maui.PushChannel";

    internal static async ValueTask<AzureNotificationHubMauiPushChannel?> GetPushChannel(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string? platformText = await SecureStorage.Default.GetAsync(PushChannelPlatformKey).ConfigureAwait(false);
        string? pushChannel = await SecureStorage.Default.GetAsync(PushChannelKey).ConfigureAwait(false);

        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(platformText) || string.IsNullOrWhiteSpace(pushChannel))
            return null;

        if (!AzureNotificationHubMauiPlatform.TryFromValue(platformText, out AzureNotificationHubMauiPlatform? platform))
            return null;

        return new AzureNotificationHubMauiPushChannel(platform, pushChannel);
    }

    internal static async ValueTask SetPushChannel(AzureNotificationHubMauiPlatform platform, string pushChannel, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(pushChannel))
            throw new ArgumentException("A push channel is required.", nameof(pushChannel));

        cancellationToken.ThrowIfCancellationRequested();

        await SecureStorage.Default.SetAsync(PushChannelPlatformKey, platform.Value).ConfigureAwait(false);
        await SecureStorage.Default.SetAsync(PushChannelKey, pushChannel).ConfigureAwait(false);

        cancellationToken.ThrowIfCancellationRequested();
    }

    internal static ValueTask ClearPushChannel(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        SecureStorage.Default.Remove(PushChannelPlatformKey);
        SecureStorage.Default.Remove(PushChannelKey);

        return ValueTask.CompletedTask;
    }
}
