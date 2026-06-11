using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Soenneker.Azure.NotificationHubs.Maui.Abstract;

namespace Soenneker.Azure.NotificationHubs.Maui;

internal sealed class NotificationHubMauiService : IAzureNotificationHubMauiService
{
    private readonly IAzureNotificationHubMauiInstallationProvider _installationProvider;
    private readonly IAzureNotificationHubMauiInstallationIdProvider _installationIdProvider;
    private readonly IAzureNotificationHubMauiPermissionService _permissionService;
    private readonly IEnumerable<IAzureNotificationHubMauiInstallationRegistrar> _registrars;
    private readonly IOptions<AzureNotificationHubsMauiOptions> _options;

    public NotificationHubMauiService(
        IAzureNotificationHubMauiInstallationProvider installationProvider,
        IAzureNotificationHubMauiInstallationIdProvider installationIdProvider,
        IAzureNotificationHubMauiPermissionService permissionService,
        IEnumerable<IAzureNotificationHubMauiInstallationRegistrar> registrars,
        IOptions<AzureNotificationHubsMauiOptions> options)
    {
        _installationProvider = installationProvider;
        _installationIdProvider = installationIdProvider;
        _permissionService = permissionService;
        _registrars = registrars;
        _options = options;
    }

    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        await _installationIdProvider.GetInstallationId(cancellationToken).ConfigureAwait(false);

        if (_options.Value.RequestPermissionOnInitialize)
            await _permissionService.RequestPermission(cancellationToken).ConfigureAwait(false);
    }

    public ValueTask<AzureNotificationHubMauiInstallation> GetCurrentInstallation(CancellationToken cancellationToken = default)
    {
        return _installationProvider.GetCurrentInstallation(cancellationToken);
    }

    public async ValueTask RegisterCurrentInstallation(CancellationToken cancellationToken = default)
    {
        IAzureNotificationHubMauiInstallationRegistrar? registrar = GetRegistrar();

        if (registrar is null)
        {
            HandleMissingRegistrar();
            return;
        }

        AzureNotificationHubMauiInstallation installation = await _installationProvider.GetCurrentInstallation(cancellationToken).ConfigureAwait(false);
        await registrar.Register(installation, cancellationToken).ConfigureAwait(false);
    }

    public async ValueTask UnregisterCurrentInstallation(CancellationToken cancellationToken = default)
    {
        IAzureNotificationHubMauiInstallationRegistrar? registrar = GetRegistrar();

        if (registrar is null)
        {
            HandleMissingRegistrar();
            return;
        }

        string installationId = await _installationIdProvider.GetInstallationId(cancellationToken).ConfigureAwait(false);
        await registrar.Unregister(installationId, cancellationToken).ConfigureAwait(false);
    }

    private IAzureNotificationHubMauiInstallationRegistrar? GetRegistrar()
    {
        return _registrars.LastOrDefault();
    }

    private void HandleMissingRegistrar()
    {
        if (!_options.Value.ThrowIfInstallationRegistrarMissing)
            return;

        throw new InvalidOperationException(
            $"No {nameof(IAzureNotificationHubMauiInstallationRegistrar)} is registered. Register an app-owned backend registrar or set {nameof(AzureNotificationHubsMauiOptions.ThrowIfInstallationRegistrarMissing)} to false.");
    }
}
