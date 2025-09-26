using Game.Fire.View;
using MVP.Presenter;
using UnityEngine;

namespace Game.Fire.Presenter
{
    public class FirePresenter: MonoBehaviour, IPresenter
    {
        [SerializeField]
        private FireView view;
        
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