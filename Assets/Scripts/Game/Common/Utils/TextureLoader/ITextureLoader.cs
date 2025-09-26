using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Common.Utils.TextureLoader
{
    public interface ITextureLoader
    {
        public Task<Texture2D> LoadAsync(string url, CancellationToken cancellationToken);
    }
}