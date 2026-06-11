[![](https://img.shields.io/nuget/v/soenneker.azure.notificationhubs.maui.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.azure.notificationhubs.maui/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.azure.notificationhubs.maui/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.azure.notificationhubs.maui/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.azure.notificationhubs.maui.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.azure.notificationhubs.maui/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Azure.NotificationHubs.Maui
### A .NET MAUI library for Azure Notification Hubs push installation lifecycle, push channel management, permission handling, and notification tap routing.

## Installation

```
dotnet add package Soenneker.Azure.NotificationHubs.Maui
```

## Registration

```csharp
using Soenneker.Azure.NotificationHubs.Maui.Registrars;

builder.Services.AddAzureNotificationHubsMaui(options =>
{
    options.ThrowIfInstallationRegistrarMissing = true;
});
```

Register an app-owned backend registrar when you want `RegisterCurrentInstallation` and `UnregisterCurrentInstallation` to call your API:

```csharp
using Soenneker.Azure.NotificationHubs.Maui;
using Soenneker.Azure.NotificationHubs.Maui.Abstract;

public sealed class PushInstallationRegistrar : IAzureNotificationHubMauiInstallationRegistrar
{
    public ValueTask Register(AzureNotificationHubMauiInstallation installation, CancellationToken cancellationToken = default)
    {
        // Send installation to your authenticated backend.
        return ValueTask.CompletedTask;
    }

    public ValueTask Unregister(string installationId, CancellationToken cancellationToken = default)
    {
        // Tell your authenticated backend to unregister the installation.
        return ValueTask.CompletedTask;
    }
}
```

```csharp
builder.Services.AddSingleton<IAzureNotificationHubMauiInstallationRegistrar, PushInstallationRegistrar>();
```

The library does not reference `Microsoft.Azure.NotificationHubs`, create Azure installations directly, send notifications, or derive Azure tags. Your backend should register with Azure Notification Hubs and derive trusted tags from authenticated identity.

## Usage

```csharp
IAzureNotificationHubMauiService service = provider.GetRequiredService<IAzureNotificationHubMauiService>();

await service.Initialize();
AzureNotificationHubMauiInstallation installation = await service.GetCurrentInstallation();
await service.RegisterCurrentInstallation();
```

`InstallationId` is a stable GUID string stored in `SecureStorage`. It is not the platform push channel.

## Android

The Android provider retrieves the FCM v1 registration token with Firebase Messaging and exposes it as `PushChannel` with `Platform = FcmV1`.

Setup requirements:

- Configure Firebase for the MAUI app before requesting the current installation.
- Add the app's `google-services.json` and any Firebase initialization required by the app.
- Request notification permission on Android 13+ with `IAzureNotificationHubMauiPermissionService` or set `RequestPermissionOnInitialize = true`.
- Ensure the Android manifest contains the notification permission required by your target SDK.

## iOS

The iOS provider returns the APNs device token captured from `AppDelegate`. Override `RegisteredForRemoteNotifications` and call the extension method:

```csharp
using Foundation;
using Soenneker.Azure.NotificationHubs.Maui;
using UIKit;

public override async void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
{
    await this.SetAzureNotificationHubsMauiApnsPushChannel(deviceToken);
}
```

Setup requirements:

- Enable Push Notifications and Remote notifications background mode in the app capabilities.
- Request notification permission before registration.
- Call `UIApplication.SharedApplication.RegisterForRemoteNotifications()` after permission is granted.

## Windows

The Windows provider attempts to create a real WNS channel URI with `PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync()`.

Setup requirements:

- Run as a packaged app with Windows app identity.
- Configure WNS in Partner Center and in the app package as required by Windows.
- If WNS is unavailable in the current app context, the provider throws a clear `PlatformNotSupportedException` instead of returning a fake channel.

## Notification taps

`IAzureNotificationHubMauiTapRouter` only raises `NotificationTapped`. It does not navigate:

```csharp
tapRouter.NotificationTapped += (_, args) =>
{
    // Decide navigation in the consuming app.
};

tapRouter.RouteTap(payload);
```
