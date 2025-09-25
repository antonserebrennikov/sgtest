using Game.Words.View;
using MVP.Presenter;
using UnityEngine;

namespace Game.Words.Presenter
{
    public class WordsPresenter: MonoBehaviour, IPresenter
    {
        [SerializeField]
        private WordsView view;
        
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