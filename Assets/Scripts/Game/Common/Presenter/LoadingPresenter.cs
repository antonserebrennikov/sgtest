using Game.Common.Utils.UI;
using Game.Common.View;
using UnityEngine;

namespace Game.Common.Presenter
{
    public class LoadingPresenter: MonoBehaviour, ILoadingPresenter
    {
        public LoadingView view;
        
        public void Awake()
        {
            if (view == null)
                Debug.LogError($"{nameof(view)} cannot be null");
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}