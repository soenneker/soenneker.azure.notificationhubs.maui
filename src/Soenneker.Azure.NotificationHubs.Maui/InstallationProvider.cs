using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Soenneker.Azure.NotificationHubs.Maui.Abstract;

namespace Soenneker.Azure.NotificationHubs.Maui;

internal sealed class InstallationProvider : IAzureNotificationHubMauiInstallationProvider
{
    private readonly IAzureNotificationHubMauiInstallationIdProvider _installationIdProvider;
    private readonly IAzureNotificationHubMauiPushChannelProvider _pushChannelProvider;
    private readonly IOptions<AzureNotificationHubsMauiOptions> _options;

    public InstallationProvider(
        IAzureNotificationHubMauiInstallationIdProvider installationIdProvider,
        IAzureNotificationHubMauiPushChannelProvider pushChannelProvider,
        IOptions<AzureNotificationHubsMauiOptions> options)
    {
        _installationIdProvider = installationIdProvider;
        _pushChannelProvider = pushChannelProvider;
        _options = options;
    }

    public async ValueTask<AzureNotificationHubMauiInstallation> GetCurrentInstallation(CancellationToken cancellationToken = default)
    {
        string installationId = await _installationIdProvider.GetInstallationId(cancellationToken).ConfigureAwait(false);
        AzureNotificationHubMauiPushChannel channel = await _pushChannelProvider.GetPushChannel(cancellationToken).ConfigureAwait(false);

        var metadata = new Dictionary<string, string>(_options.Value.Metadata, System.StringComparer.Ordinal);

        return new AzureNotificationHubMauiInstallation
        {
            InstallationId = installationId,
            Platform = channel.Platform,
            PushChannel = channel.PushChannel,
            AppIdentifier = AppInfo.Current.PackageName,
            AppName = AppInfo.Current.Name,
            AppVersion = AppInfo.Current.VersionString,
            AppBuild = AppInfo.Current.BuildString,
            DeviceManufacturer = DeviceInfo.Current.Manufacturer,
            DeviceModel = DeviceInfo.Current.Model,
            DeviceName = DeviceInfo.Current.Name,
            DevicePlatform = DeviceInfo.Current.Platform.ToString(),
            DeviceVersion = DeviceInfo.Current.VersionString,
            DeviceIdiom = DeviceInfo.Current.Idiom.ToString(),
            DeviceType = DeviceInfo.Current.DeviceType.ToString(),
            Metadata = metadata
        };
    }
}
