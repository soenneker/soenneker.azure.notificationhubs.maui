using System.Collections.Generic;

namespace Soenneker.Azure.NotificationHubs.Maui;

public sealed class AzureNotificationHubsMauiOptions
{
    public bool ThrowIfInstallationRegistrarMissing { get; set; } = true;

    public bool RequestPermissionOnInitialize { get; set; }

    public Dictionary<string, string> Metadata { get; } = [];
}
