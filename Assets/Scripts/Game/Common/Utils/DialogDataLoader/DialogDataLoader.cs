using System;
using System.Threading;
using System.Threading.Tasks;
using Game.Common.Utils.DialogData;
using UnityEngine.Networking;

namespace Game.Common.Utils.DialogDataLoader
{
    public class DialogDataLoader: IDialogDataLoader
    {
        public async Task<DialogPayload> LoadAsync(string url, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");
            
            using var request = UnityWebRequest.Get(url);
            var op = request.SendWebRequest();

            // Await completion or cancellation.
            await AwaitRequestAsync(op, cancellationToken);
            
            if (request.result != UnityWebRequest.Result.Success)
            {
                // If the request was aborted due to cancellation, surface a cancellation exception.
                if (cancellationToken.IsCancellationRequested)
                    throw new OperationCanceledException("Dialog data loading was canceled.", cancellationToken);

                throw new Exception($"Failed to load dialog data. HTTP: {request.responseCode}, Error: {request.error}");
            }

            var json = request.downloadHandler?.text ?? string.Empty;
                
            if (string.IsNullOrWhiteSpace(json))
                throw new Exception("Dialog data response was empty.");

            var payload = DialogDataFactory.FromJson(json);
                
            if (payload == null)
                throw new Exception("Failed to parse dialog data payload.");

            return payload;
        }

        private Task AwaitRequestAsync(UnityWebRequestAsyncOperation operation, CancellationToken ct)
        {
            if (operation == null) throw new ArgumentNullException(nameof(operation));

            var tcs = new TaskCompletionSource<bool>();

            // Handle completion
            Action<UnityEngine.AsyncOperation> completed = null;
            completed = _ =>
            {
                operation.completed -= completed;
                tcs.TrySetResult(true);
            };
            operation.completed += completed;

            // Handle cancellation
            if (ct.CanBeCanceled)
            {
                ct.Register(() =>
                {
                    try
                    {
                        operation.webRequest?.Abort();
                    }
                    catch
                    {
                        // ignored
                    }
                    tcs.TrySetCanceled(ct);
                });
            }

            // Fast-path if already done
            if (operation.isDone)
            {
                operation.completed -= completed;
                tcs.TrySetResult(true);
            }

            return tcs.Task;
        }
    }
}