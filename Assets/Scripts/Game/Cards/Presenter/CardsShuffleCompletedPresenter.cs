using Game.Cards.View;
using MVP.Presenter;
using UnityEngine;

namespace Game.Cards.Presenter
{
    public class CardsShuffleCompletedPresenter: MonoBehaviour, IPresenter
    {
        [SerializeField]
        private CardsShuffleCompletedView view;
        
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