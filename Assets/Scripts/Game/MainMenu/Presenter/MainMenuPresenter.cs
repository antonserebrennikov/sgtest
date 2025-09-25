using System;
using Game.MainMenu.View;
using MVP.Presenter;
using UnityEngine;

namespace Game.MainMenu.Presenter
{
    public class MainMenuPresenter: MonoBehaviour, IPresenter
    {
        public enum SceneName
        {
            CardsScene,
            WordsScene,
            FireScene
        }
        
        [SerializeField]
        private MainMenuView view;

        public Action<SceneName> OnSceneLoad;
        
        public void Show()
        {
            gameObject.SetActive(true);

            if (view == null) return;
            
            view.OnCardsButton += OnCardsButtonClick;
            view.OnWordsButton += OnWordsButtonClick;
            view.OnFireButton += OnFireButtonClick;
        }

        public void Hide()
        {
            gameObject.SetActive(false);

            if (view == null) return;
            
            view.OnCardsButton -= OnCardsButtonClick;
            view.OnWordsButton -= OnWordsButtonClick;
            view.OnFireButton -= OnFireButtonClick;
        }
        
        private void OnCardsButtonClick()
        {
            OnSceneLoad?.Invoke(SceneName.CardsScene);
        }
        
        private void OnWordsButtonClick()
        {
            OnSceneLoad?.Invoke(SceneName.WordsScene);
        }
        
        private void OnFireButtonClick()
        {
            OnSceneLoad?.Invoke(SceneName.FireScene);
        }
    }
}