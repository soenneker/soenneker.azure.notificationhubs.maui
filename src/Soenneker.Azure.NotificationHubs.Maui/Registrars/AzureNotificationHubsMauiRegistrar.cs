using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Azure.NotificationHubs.Maui.Abstract;
    /// <summary>
    /// Registers Azure Notification Hubs Maui with the service collection.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <param name="configure">Callback that configures Notification Hubs for the MAUI application.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
#if ANDROID
using Soenneker.Azure.NotificationHubs.Maui.Platforms.Android;
#endif
    /// <summary>
    /// Registers Azure Notification Hubs Maui with the service collection.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <param name="configure">Callback that configures Notification Hubs for the MAUI application.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
#if IOS
using Soenneker.Azure.NotificationHubs.Maui.Platforms.Ios;
#endif
    /// <summary>
    /// Registers Azure Notification Hubs Maui with the service collection.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <param name="configure">Callback that configures Notification Hubs for the MAUI application.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
#if WINDOWS
using Soenneker.Azure.NotificationHubs.Maui.Platforms.Windows;
#endif

namespace Soenneker.Azure.NotificationHubs.Maui.Registrars;

public static class AzureNotificationHubsMauiRegistrar
{
    /// <summary>
    /// Registers Azure Notification Hubs Maui with the service collection.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <param name="configure">Callback used to configure the registered service or operation.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
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
