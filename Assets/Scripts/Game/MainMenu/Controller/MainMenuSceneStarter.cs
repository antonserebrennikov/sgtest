using System;
using System.Threading.Tasks;
using Game.Common.Utils.Assets.Scene;
using Game.Common.Utils.UI;
using Game.MainMenu.Presenter;
using Game.Scene;
using Jnk.TinyContainer;
using UnityEngine;

namespace Game.MainMenu.Controller
{
    public class MainMenuSceneStarter: MonoBehaviour
    {
        private ILoadingPresenter loadingPresenter;
        private IPresenterLoader presenterLoader;
        private ISceneLoader sceneLoader;
        private MainMenuPresenter mainMenuPresenter;
        
        public void Awake()
        {
            TinyContainer.For(this).Get(out loadingPresenter);
            TinyContainer.For(this).Get(out presenterLoader);
            TinyContainer.For(this).Get(out sceneLoader);
        }
        
        public void Start()
        {
            _ = InitAsync();
        }
        
        private async Task InitAsync()
        {
            try
            {
                mainMenuPresenter = await presenterLoader.LoadPresenterAsync<MainMenuPresenter>();
                mainMenuPresenter.OnSceneLoad += OnSceneLoadClick;
                mainMenuPresenter.Show();
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

        private void OnSceneLoadClick(MainMenuPresenter.SceneName sceneName)
        {
            mainMenuPresenter.OnSceneLoad -= OnSceneLoadClick;
            mainMenuPresenter.Hide();
            
            switch (sceneName)
            {
                case MainMenuPresenter.SceneName.CardsScene:
                    _ = LoadScene(Scenes.Cards);
                    break;
                case MainMenuPresenter.SceneName.WordsScene:
                    _ = LoadScene(Scenes.Words);
                    break;
                case MainMenuPresenter.SceneName.FireScene:
                    _ = LoadScene(Scenes.Fire);
                    break;
                default:
                    Debug.LogError($"Scene '{sceneName}' is not supported");
                    break;
            }
        }
        
        private async Task LoadScene(string sceneName)
        {
            try
            {
                loadingPresenter.Show();
                
                await sceneLoader.LoadSceneAsync(sceneName);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}