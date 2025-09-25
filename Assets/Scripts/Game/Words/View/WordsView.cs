using MVP.View;
using TMPro;
using UnityEngine;

namespace Game.Words.View
{
    public class WordsView: MonoBehaviour, IView
    {
        [SerializeField][Header("Dialog text")]
        private TMP_Text dialogText;

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
    }
}