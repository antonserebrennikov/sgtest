using System;
using System.Linq;
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
        private const string AvatarLocationLeft = "left";
        
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
            {
                view.SetDialogText("");
                view.SetButtonVisibility(false);
                view.SetAvatarsVisibility(false);
                view.OnNextButton += OnNextButtonHandler;
            }
        }

        public void Hide()
        {
            if (view != null)
                view.OnNextButton -= OnNextButtonHandler;
            
            gameObject.SetActive(false);
        }

        public async Task ShowAsync()
        {
            try
            {
                Show();
                data ??= await wordsModel.GetDialogData();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                ShowError(NoDataLoadedError);
                throw;
            }
                
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
            
            var avatar = data.avatars.FirstOrDefault(a => a.name == dialog.name);

            if (avatar != null)
            {
                view.SetDialogAvatar(avatar.position == AvatarLocationLeft ? WordsView.AvatarSide.Left : WordsView.AvatarSide.Right, null);
            }
        }
        
        private void OnNextButtonHandler()
        {
            currentDialogIndex++;
            ShowDialog(currentDialogIndex);
        }

        private void ShowError(string message)
        {
            if (view != null)
            {
                view.SetDialogText(NoDataLoadedError);
                view.SetButtonVisibility(false);
            }
        }
    }
}