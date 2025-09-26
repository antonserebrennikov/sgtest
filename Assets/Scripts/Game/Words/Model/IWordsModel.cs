using System.Threading.Tasks;
using Game.Common.Utils.DialogData;
using Game.Common.Utils.DialogDataLoader;
using MVP.Model;

namespace Game.Words.Model
{
    public interface IWordsModel: IModel
    {
        public Task<DialogPayload> GetDialogData();
    }
}