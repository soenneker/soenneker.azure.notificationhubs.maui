using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Azure.NotificationHubs.Maui.Abstract;

#if ANDROID
using Soenneker.Azure.NotificationHubs.Maui.Platforms.Android;
#endif

#if IOS
using Soenneker.Azure.NotificationHubs.Maui.Platforms.Ios;
#endif

#if WINDOWS
using Soenneker.Azure.NotificationHubs.Maui.Platforms.Windows;
#endif

namespace Soenneker.Azure.NotificationHubs.Maui.Registrars;

public static class AzureNotificationHubsMauiRegistrar
{
    public static IServiceCollection AddAzureNotificationHubsMaui(this IServiceCollection services, Action<AzureNotificationHubsMauiOptions>? configure = null)
    {
        services.AddOptions<AzureNotificationHubsMauiOptions>();

        if (configure is not null)
            services.Configure(configure);

        services.TryAddSingleton<IAzureNotificationHubMauiService, NotificationHubMauiService>();
        services.TryAddSingleton<IAzureNotificationHubMauiInstallationProvider, InstallationProvider>();
        services.TryAddSingleton<IAzureNotificationHubMauiInstallationIdProvider, InstallationIdProvider>();
        services.TryAddSingleton<IAzureNotificationHubMauiPushChannelStore, PushChannelStore>();
        services.TryAddSingleton<IAzureNotificationHubMauiPermissionService, PermissionService>();
        services.TryAddSingleton<IAzureNotificationHubMauiTapRouter, TapRouter>();

#if ANDROID
        services.TryAddSingleton<IAzureNotificationHubMauiPushChannelProvider, FcmV1PushChannelProvider>();
#endif

#if IOS
        services.TryAddSingleton<IAzureNotificationHubMauiPushChannelProvider, ApnsPushChannelProvider>();
#endif

#if WINDOWS
        services.TryAddSingleton<IAzureNotificationHubMauiPushChannelProvider, WnsPushChannelProvider>();
#endif

        return services;
    }
}
