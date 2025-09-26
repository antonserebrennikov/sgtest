using System;
using System.Threading.Tasks;
using Game.Common.Utils.DialogData;
using Game.Common.Utils.DialogDataLoader;

namespace Game.Words.Model
{
    public class WordsModel: IWordsModel
    {
        private IDialogDataLoader dialogDataLoader;
        private DialogPayload dialogPayload;
        
        public WordsModel(IDialogDataLoader dialogDataLoader)
        {
            this.dialogDataLoader = dialogDataLoader ?? throw new ArgumentNullException(nameof(dialogDataLoader));
        }

        public async Task<DialogPayload> GetDialogData()
        {
            if (dialogPayload != null)
                return dialogPayload;
            
            if (dialogDataLoader == null)
                throw new Exception("Mission data loader is not set");
            
            dialogPayload = await dialogDataLoader.LoadAsync();
            
            return dialogPayload;
        }
    }
}