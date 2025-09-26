using System;
using System.Threading.Tasks;
using Game.Common.Controller;
using Game.Common.Utils.UI;
using Game.Words.Model;
using Game.Words.Presenter;
using Jnk.TinyContainer;
using UnityEngine;

namespace Game.Words.Controller
{
    public class WordsSceneStarter: BackButtonController
    {
        private ILoadingPresenter loadingPresenter;
        private IWordsModel wordsModel;
        private WordsPresenter wordsPresenter;
        
        protected override async Task InitAsync()
        {
            TinyContainer.For(this).Get(out loadingPresenter);
            TinyContainer.For(this).Get(out wordsModel);

            try
            {
                var data = await wordsModel.GetDialogData();
                
                Debug.Log($"Dialog data loaded successfully {data}");
                
                wordsPresenter = await presenterLoader.LoadPresenterAsync<WordsPresenter>();
                wordsPresenter.Show();
            
                loadingPresenter.Hide();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        protected override async Task DeinitAsync()
        {
            loadingPresenter.Show();
            wordsPresenter?.Hide();
        }
    }
}