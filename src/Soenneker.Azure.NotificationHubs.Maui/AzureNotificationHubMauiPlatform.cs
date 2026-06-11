using Soenneker.Gen.EnumValues;

namespace Soenneker.Azure.NotificationHubs.Maui;

[EnumValue<string>]
public sealed partial class AzureNotificationHubMauiPlatform
{
    public static readonly AzureNotificationHubMauiPlatform FcmV1 = new(nameof(FcmV1));

    public static readonly AzureNotificationHubMauiPlatform Apns = new(nameof(Apns));

    public static readonly AzureNotificationHubMauiPlatform Wns = new(nameof(Wns));
}
