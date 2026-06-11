using System.Collections.Generic;

namespace Soenneker.Azure.NotificationHubs.Maui;

public sealed class AzureNotificationHubMauiInstallation
{
    public string InstallationId { get; init; } = "";

    public AzureNotificationHubMauiPlatform Platform { get; init; } = null!;

    public string PushChannel { get; init; } = "";

    public string AppIdentifier { get; init; } = "";

    public string? AppName { get; init; }

    public string? AppVersion { get; init; }

    public string? AppBuild { get; init; }

    public string? DeviceManufacturer { get; init; }

    public string? DeviceModel { get; init; }

    public string? DeviceName { get; init; }

    public string? DevicePlatform { get; init; }

    public string? DeviceVersion { get; init; }

    public string? DeviceIdiom { get; init; }

    public string? DeviceType { get; init; }

    public Dictionary<string, string> Metadata { get; init; } = [];
}
