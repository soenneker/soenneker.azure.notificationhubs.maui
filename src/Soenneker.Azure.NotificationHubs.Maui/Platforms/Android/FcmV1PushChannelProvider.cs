#if ANDROID
using System;
using System.Threading;
using System.Threading.Tasks;
using Firebase.Messaging;
using Soenneker.Azure.NotificationHubs.Maui.Abstract;

using AndroidTask = Android.Gms.Tasks.Task;

namespace Soenneker.Azure.NotificationHubs.Maui.Platforms.Android;

/// <inheritdoc cref="IAzureNotificationHubMauiPushChannelProvider" />
internal sealed class FcmV1PushChannelProvider : IAzureNotificationHubMauiPushChannelProvider
{
    private readonly IAzureNotificationHubMauiPushChannelStore _store;

    public FcmV1PushChannelProvider(IAzureNotificationHubMauiPushChannelStore store)
    {
        _store = store;
    }

    public async ValueTask<AzureNotificationHubMauiPushChannel> GetPushChannel(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        AndroidTask tokenTask;

        try
        {
            tokenTask = FirebaseMessaging.Instance.GetToken();
        }
        catch (Exception exception)
        {
            throw new InvalidOperationException("Unable to request the Firebase Messaging push channel. Ensure Firebase is initialized before requesting the current installation.", exception);
        }

        string? pushChannel = await AwaitPushChannel(tokenTask, cancellationToken).ConfigureAwait(false);

        if (string.IsNullOrWhiteSpace(pushChannel))
            throw new InvalidOperationException("Firebase Messaging returned an empty push channel.");

        await _store.Set(AzureNotificationHubMauiPlatform.FcmV1, pushChannel, cancellationToken).ConfigureAwait(false);

        return new AzureNotificationHubMauiPushChannel(AzureNotificationHubMauiPlatform.FcmV1, pushChannel);
    }

    private static async Task<string?> AwaitPushChannel(AndroidTask tokenTask, CancellationToken cancellationToken)
    {
        var completion = new TaskCompletionSource<string?>(TaskCreationOptions.RunContinuationsAsynchronously);
        using CancellationTokenRegistration registration = cancellationToken.Register(() => completion.TrySetCanceled(cancellationToken));

        tokenTask.AddOnCompleteListener(new FirebaseMessagingTokenCompleteListener(completion));

        return await completion.Task.ConfigureAwait(false);
    }
}
#endif
