using System;
using MVP.View;
using UnityEngine;

namespace Game.MainMenu.View
{
    public class MainMenuView: MonoBehaviour, IView
    {
        public Action OnCardsButton;
        public Action OnWordsButton;
        public Action OnFireButton;
        
        public void OnCardsButtonClick()
        {
            OnCardsButton?.Invoke();
        }
        
        public void OnWordsButtonClick()
        {
            OnWordsButton?.Invoke();
        }
        
        public void OnFireButtonClick()
        {
            OnFireButton?.Invoke();
        }
    }
}