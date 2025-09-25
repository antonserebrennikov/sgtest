using System;
using TMPro;
using UnityEngine;

namespace Game.Cards.View
{
    public class CardStackView: MonoBehaviour
    {
        [SerializeField][Header("Items counter")]
        private TMP_Text counterText;
        
        [SerializeField][Header("Items placeholder")]
        private Transform placeholder;
        
        private Vector2 cardsOffset = Vector2.zero;
        
        public void SetCardsOdffset(Vector2 offset)
        {
            cardsOffset = offset;
        }
        
        private void Awake()
        {
            if (placeholder == null)
                throw new ArgumentNullException(nameof(placeholder), "Placeholder cannot be null.");
        }
        
        public void UpdateCounterText(int count)
        {
            if (counterText != null)
                counterText.text = count.ToString();
        }
        
        public void Push(GameObject card, int count)
        {
            if (card == null)
            {
                Debug.LogError("Attempted to push a null card.");
                return;
            }

            if (placeholder != null)
            {
                card.transform.SetParent(placeholder, worldPositionStays: false);
                card.transform.localPosition = GetNextCardPosition(count);
            }
        }
        
        public Vector3 GetNextCardPosition(int cardsCount)
        {
            if (placeholder == null)
            {
                Debug.LogError("Stack root is not set. Cannot determine next card position.");
                return Vector3.zero;
            }
            
            return placeholder.position + new Vector3(cardsOffset.x * cardsCount, cardsOffset.y * cardsCount, -0.001f * cardsCount);
        }
    }
}