using System;
using System.Threading.Tasks;
using Game.Common.Presenter;
using Game.Common.Utils.Assets.Scene;
using Game.Common.Utils.UI;
using Game.Scene;
using Jnk.TinyContainer;
using UnityEngine;

namespace Game.Common.Controller
{
    public abstract class BackButtonController: MonoBehaviour
    {
        protected IPresenterLoader presenterLoader;
        protected ISceneLoader sceneLoader;
        protected BackButtonPresenter backButtonPresenter;
        
        public void Awake()
        {
            TinyContainer.For(this).Get(out presenterLoader);
            TinyContainer.For(this).Get(out sceneLoader);
        }
        
        public void Start()
        {
            _ = InitAsyncInternal();
        }
        
        private async Task InitAsyncInternal()
        {
            try
            {
                backButtonPresenter = await presenterLoader.LoadPresenterAsync<BackButtonPresenter>();
                backButtonPresenter.OnBackButton += OnBackButtonClick;
                backButtonPresenter.Show();
                
                await InitAsync();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        protected abstract Task InitAsync();
        
        protected abstract Task DeinitAsync();

        private void OnBackButtonClick()
        {
            backButtonPresenter.OnBackButton -= OnBackButtonClick;
            backButtonPresenter.Hide();
            _ = LoadScene(Scenes.MainMenu);
        }
        
        private async Task LoadScene(string sceneName)
        {
            try
            {
                await DeinitAsync();
                await sceneLoader.LoadSceneAsync(sceneName);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}