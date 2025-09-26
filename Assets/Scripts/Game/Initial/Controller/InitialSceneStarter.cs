using System;
using System.Threading.Tasks;
using Game.Common.Presenter;
using Game.Common.Utils.Assets.Prefab;
using Game.Common.Utils.Assets.Scene;
using Game.Common.Utils.DialogDataLoader;
using Game.Common.Utils.UI;
using Game.Scene;
using Game.Words.Model;
using Jnk.TinyContainer;
using UnityEngine;

namespace Game.Initial.Controller
{
    public class InitialSceneStarter: MonoBehaviour
    {
        [Range(30, 120)]
        public int TargetFps = 60;
        
        private ISceneLoader sceneLoader;
        private IPrefabLoader prefabLoader;
        private IPresenterLoader presenterLoader;
        
        private void Awake()
        {
            Application.targetFrameRate = TargetFps;
            RegisterGlobal();
        }

        private void Start()
        {
            Resolve();
            _ = InitAsync();
        }
        
        private void RegisterGlobal()
        {
            TinyContainer.Global.Register<ISceneLoader>(new SceneLoader());
            
            var prefabLoader = new AddressablePrefabLoader();
            var dialogDataLoader = new DialogDataLoader();
            
            TinyContainer.Global.Register<IPrefabLoader>(prefabLoader);
            TinyContainer.Global.Register<IPresenterLoader>(new PresenterLoader(prefabLoader));
            TinyContainer.Global.Register<IWordsModel>(new WordsModel(dialogDataLoader));
        }
        
        private void Resolve()
        {
            TinyContainer.For(this).Get(out sceneLoader);
            TinyContainer.For(this).Get(out prefabLoader);
            TinyContainer.For(this).Get(out presenterLoader);
        }
        
        private async Task InitAsync()
        {
            try
            {
                var loadingPresenter = await presenterLoader.LoadPresenterAsync<LoadingPresenter>();
            
                TinyContainer.Global.Register<ILoadingPresenter>(loadingPresenter);
                
                await sceneLoader.LoadSceneAsync(Scenes.MainMenu);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}