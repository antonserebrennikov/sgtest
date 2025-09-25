using System;
using Game.Common.View;
using MVP.Presenter;
using UnityEngine;

namespace Game.Common.Presenter
{
    public class BackButtonPresenter: MonoBehaviour, IPresenter
    {
        [SerializeField]
        private BackButtonView view;
        
        public Action OnBackButton;
        
        public void Awake()
        {
            if (view == null)
                Debug.LogError($"{nameof(view)} cannot be null");
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
            
            if (view != null)
                view.OnBackButton += OnBackButtonClick;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            
            if (view != null)
                view.OnBackButton += OnBackButtonClick;
        }
        
        private void OnBackButtonClick()
        {
            OnBackButton?.Invoke();
        }
    }
}