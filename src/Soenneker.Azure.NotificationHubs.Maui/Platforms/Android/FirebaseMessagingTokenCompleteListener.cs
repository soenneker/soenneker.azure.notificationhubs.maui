#if ANDROID
using System;
using System.Threading.Tasks;

using AndroidOnCompleteListener = Android.Gms.Tasks.IOnCompleteListener;
using AndroidTask = Android.Gms.Tasks.Task;

namespace Soenneker.Azure.NotificationHubs.Maui.Platforms.Android;

internal sealed class FirebaseMessagingTokenCompleteListener : global::Java.Lang.Object, AndroidOnCompleteListener
{
    private readonly TaskCompletionSource<string?> _completion;

    internal FirebaseMessagingTokenCompleteListener(TaskCompletionSource<string?> completion)
    {
        _completion = completion;
    }

    public void OnComplete(AndroidTask task)
    {
        if (task.IsCanceled)
        {
            _completion.TrySetCanceled();
            return;
        }

        if (!task.IsSuccessful)
        {
            Exception? exception = task.Exception;

            _completion.TrySetException(exception is null
                ? new InvalidOperationException("Firebase Messaging did not complete successfully.")
                : new InvalidOperationException("Firebase Messaging did not complete successfully.", exception));

            return;
        }

        _completion.TrySetResult(task.Result?.ToString());
    }
}
#endif
