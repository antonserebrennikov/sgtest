using System;
using MVP.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Words.View
{
    public class WordsView: MonoBehaviour, IView
    {
        public enum AvatarSide
        {
            Left,
            Right
        }
        
        [Header("Dialog text")]
        [SerializeField]
        private TMP_Text dialogText;
        
        [Header("Next button")]
        [SerializeField]
        private GameObject nextButton;
        
        [Header("Dynamically loaded avatars")]
        [SerializeField]
        private RawImage leftImage;
        [SerializeField]
        private RawImage rightImage;
        
        [Header("Default avatars")]
        [SerializeField]
        private GameObject leftDefaultAvatar;
        [SerializeField]
        private GameObject rightDefaultAvatar;
        
        public Action OnNextButton;

        private void Awake()
        {
            if (dialogText == null)
                Debug.LogError($"{nameof(dialogText)} cannot be null");
            
            if (nextButton == null)
                Debug.LogError($"{nameof(nextButton)} cannot be null");
            
            ValidateAvatars();
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
        
        public void SetAvatarsVisibility(bool isVisible)
        {
            if (leftImage == null || rightImage == null || leftDefaultAvatar == null || rightDefaultAvatar == null)
            {
                Debug.LogError("One or more avatar components are null");
                return;
            }
            
            leftImage.gameObject.SetActive(isVisible);
            rightImage.gameObject.SetActive(isVisible);
            leftDefaultAvatar.SetActive(isVisible);
            rightDefaultAvatar.SetActive(isVisible);
        }
        
        public void SetDialogAvatar(AvatarSide side, Texture2D avatarTexture)
        {
            SetAvatarsVisibility(false);
            
            switch (side)
            {
                case AvatarSide.Left when avatarTexture != null:
                    if (leftImage == null) break;
                    leftImage.texture = avatarTexture;
                    leftImage.gameObject.SetActive(true);
                    break;
                case AvatarSide.Left:
                    leftDefaultAvatar?.SetActive(true);
                    break;
                case AvatarSide.Right when avatarTexture != null:
                    if (rightImage == null) break;
                    rightImage.texture = avatarTexture;
                    rightImage.gameObject.SetActive(true);
                    break;
                case AvatarSide.Right:
                    rightDefaultAvatar.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(side), side, null);
            }
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

        private void ValidateAvatars()
        {
            if (leftImage == null)
                Debug.LogError($"{nameof(leftImage)} cannot be null");
            
            if (rightImage == null)
                Debug.LogError($"{nameof(rightImage)} cannot be null");
            
            if (leftDefaultAvatar == null)
                Debug.LogError($"{nameof(leftDefaultAvatar)} cannot be null");
            
            if (rightDefaultAvatar == null)
                Debug.LogError($"{nameof(rightDefaultAvatar)} cannot be null");
        }
    }
}