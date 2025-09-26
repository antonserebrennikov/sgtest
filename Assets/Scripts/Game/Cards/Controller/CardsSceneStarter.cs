using System;
using System.Threading.Tasks;
using Game.Cards.Presenter;
using Game.Common.Controller;
using Game.Common.Utils.UI;
using Jnk.TinyContainer;
using UnityEngine;

namespace Game.Cards.Controller
{
    public class CardsSceneStarter: BackButtonController
    {
        [SerializeField]
        private CardsController cardsController;
        
        private ILoadingPresenter loadingPresenter;
        
        protected override async Task InitAsync()
        {
            TinyContainer.For(this).Get(out loadingPresenter);
            
            try
            {
                var shuffleCompletePresenter = await presenterLoader.LoadPresenterAsync<CardsShuffleCompletedPresenter>();

                shuffleCompletePresenter.Hide();
                TinyContainer.ForSceneOf(this).Register(shuffleCompletePresenter);

                if (cardsController != null)
                    await cardsController.InitAsync();
                else
                    Debug.LogError($"{nameof(cardsController)} is null");
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
            finally
            {
                loadingPresenter.Hide();
            }
        }

        protected override async Task DeinitAsync()
        {
            loadingPresenter.Show();
            
            if (cardsController != null)
                await cardsController.DeinitAsync();
        }
    }
}