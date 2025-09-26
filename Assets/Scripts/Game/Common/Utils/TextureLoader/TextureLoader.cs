using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Game.Common.Utils.TextureLoader
{
    public class TextureLoader: ITextureLoader
    {
        public async Task<Texture2D> LoadAsync(string url, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL is null or empty.", nameof(url));

            using var request = UnityWebRequestTexture.GetTexture(url);
            
            request.disposeDownloadHandlerOnDispose = true;

            var operation = request.SendWebRequest();
            
            // Await with cancellation support
            try
            {
                while (!operation.isDone)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await Task.Yield();
                }
            }
            catch (OperationCanceledException)
            {
                request.Abort();
                throw;
            }

            if (request.result != UnityWebRequest.Result.Success)
            {
                throw new Exception($"Failed to load texture from '{url}': {request.error}");
            }

            // GetTexture creates a Texture2D configured as requested
            var texture = DownloadHandlerTexture.GetContent(request);

            if (texture == null)
                throw new Exception($"Failed to decode texture from '{url}'.");

            return texture;
        }
    }
}