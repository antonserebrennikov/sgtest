using System.Threading.Tasks;
using Game.Common.Controller;
using Game.Common.Utils.UI;
using Game.Fire.Presenter;
using Jnk.TinyContainer;
using UnityEngine;

namespace Game.Fire.Controller
{
    public class FireSceneStarter: BackButtonController
    {
        [SerializeField]
        private FirePresenter firePresenter;
        private ILoadingPresenter loadingPresenter;
        
        protected override Task InitAsync()
        {
            TinyContainer.For(this).Get(out loadingPresenter);
            
            loadingPresenter.Hide();
            
            return Task.CompletedTask;
        }

        protected override Task DeinitAsync()
        {
            loadingPresenter.Show();
            
            return Task.CompletedTask;
        }
    }
}