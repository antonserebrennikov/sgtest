using System.Threading;
using System.Threading.Tasks;
using Game.Common.Utils.DialogData;

namespace Game.Common.Utils.DialogDataLoader
{
    public interface IDialogDataLoader
    {
        public Task<DialogPayload> LoadAsync(string url, CancellationToken cancellationToken);
    }
}