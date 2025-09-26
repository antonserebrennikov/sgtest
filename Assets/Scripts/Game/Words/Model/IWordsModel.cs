using System.Threading.Tasks;
using Game.Common.Utils.DialogData;
using MVP.Model;

namespace Game.Words.Model
{
    public interface IWordsModel: IModel
    {
        public Task<DialogPayload> GetDialogData();
    }
}