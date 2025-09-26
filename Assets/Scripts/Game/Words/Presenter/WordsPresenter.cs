using System.Threading.Tasks;
using Game.Common.Utils.DialogData;
using Game.Words.Model;
using Game.Words.View;
using Jnk.TinyContainer;
using MVP.Presenter;
using UnityEngine;

namespace Game.Words.Presenter
{
    public class WordsPresenter: MonoBehaviour, IPresenter
    {
        private const string NoDataLoadedError = "No data loaded";
        
        [SerializeField]
        private WordsView view;
        
        private IWordsModel wordsModel;
        private DialogPayload data;
        private int currentDialogIndex;
        
        public void Awake()
        {
            if (view == null)
                Debug.LogError($"{nameof(view)} cannot be null");
            
            TinyContainer.For(this).Get(out wordsModel);
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
            currentDialogIndex = 0;
            
            if (view != null)
                view.OnNextButton += OnNextButtonHandler;
            
            _ = ShowAsync();
        }

        public void Hide()
        {
            if (view != null)
                view.OnNextButton -= OnNextButtonHandler;
            
            gameObject.SetActive(false);
        }

        private async Task ShowAsync()
        {
            if (data == null)
                data = await wordsModel.GetDialogData();
                
            if (data == null || data.dialogue.Count <= 0)
                view.SetDialogText(NoDataLoadedError);
            else
                ShowDialog(currentDialogIndex);
        }
        
        private void ShowDialog(int index)
        {
            if (index >= data.dialogue.Count)
                return;
            
            var dialog = data.dialogue[index];
            
            view.SetDialogText(dialog.text);
            view.SetButtonVisibility(index < data.dialogue.Count - 1);
        }
        
        private void OnNextButtonHandler()
        {
            currentDialogIndex++;
            ShowDialog(currentDialogIndex);
        }
    }
}