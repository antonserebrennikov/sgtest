using System;
using MVP.View;
using UnityEngine;

namespace Game.Common.View
{
    public class BackButtonView: MonoBehaviour, IView
    {
        public Action OnBackButton;
        
        public void OnBackButtonClick()
        {
            OnBackButton?.Invoke();
        }
    }
}