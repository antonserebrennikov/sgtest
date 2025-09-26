using System;
using MVP.View;
using TMPro;
using UnityEngine;

namespace Game.Words.View
{
    public class WordsView: MonoBehaviour, IView
    {
        [SerializeField][Header("Dialog text")]
        private TMP_Text dialogText;
        
        [SerializeField][Header("Next button")]
        private GameObject nextButton;
        
        public Action OnNextButton;

        private void Awake()
        {
            if (dialogText == null)
                Debug.LogError($"{nameof(dialogText)} cannot be null");
        }

        public void SetDialogText(string text)
        {
            if (dialogText == null)
            {
                Debug.LogError($"{nameof(dialogText)} cannot be null");
                return;
            }
            
            dialogText.text = text;
        }
        
        public void SetButtonVisibility(bool isVisible)
        {
            if (nextButton == null)
            {
                Debug.LogError($"{nameof(nextButton)} is null");
                return;
            }
            
            nextButton.SetActive(isVisible);
        }
        
        public void OnNextButtonClick()
        {
            OnNextButton?.Invoke();
        }
    }
}