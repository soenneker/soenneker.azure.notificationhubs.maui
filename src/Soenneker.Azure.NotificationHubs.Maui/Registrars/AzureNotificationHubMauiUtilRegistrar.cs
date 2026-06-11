using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Azure.NotificationHubs.Maui.Abstract;

namespace Soenneker.Azure.NotificationHubs.Maui.Registrars;

/// <summary>
/// A .NET MAUI library for push notification registration, device installation tracking, token lifecycle management, and notification tap routing.
/// </summary>
public static class AzureNotificationHubMauiUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="IAzureNotificationHubMauiUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddAzureNotificationHubMauiUtilAsSingleton(this IServiceCollection services)
    {
        services.TryAddSingleton<IAzureNotificationHubMauiUtil, AzureNotificationHubMauiUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IAzureNotificationHubMauiUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddAzureNotificationHubMauiUtilAsScoped(this IServiceCollection services)
    {
        services.TryAddScoped<IAzureNotificationHubMauiUtil, AzureNotificationHubMauiUtil>();

        return services;
    }
}
