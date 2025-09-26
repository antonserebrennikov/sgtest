using System;
using System.Threading.Tasks;
using Game.Common.Controller;
using Game.Common.Utils.UI;
using Game.Words.Presenter;
using Jnk.TinyContainer;

namespace Game.Words.Controller
{
    public class WordsSceneStarter: BackButtonController
    {
        private ILoadingPresenter loadingPresenter;
        private WordsPresenter wordsPresenter;
        
        protected override async Task InitAsync()
        {
            TinyContainer.For(this).Get(out loadingPresenter);

            try
            {
                wordsPresenter = await presenterLoader.LoadPresenterAsync<WordsPresenter>();
                await wordsPresenter.ShowAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                loadingPresenter.Hide();
            }
        }

        protected override Task DeinitAsync()
        {
            loadingPresenter.Show();
            wordsPresenter?.Hide();
            return Task.CompletedTask;
        }
    }
}