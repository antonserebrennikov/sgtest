using System.Threading.Tasks;
using Game.Common.Controller;
using Game.Common.Utils.UI;
using Jnk.TinyContainer;

namespace Game.Fire.Controller
{
    public class FireSceneStarter: BackButtonController
    {
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