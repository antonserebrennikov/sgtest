using System.Collections.Generic;
using Game.Cards.View;
using UnityEngine;

namespace Game.Cards.Presenter
{
    public class CardsStackPresenter: MonoBehaviour
    {
        [SerializeField]
        private CardStackView view;
        
        private readonly Stack<CardItem> stack = new();
        
        public int Count => stack.Count;

        public void Init(Vector2 cardsOffset)
        {
            if (view != null)
                view.SetCardsOdffset(cardsOffset);
            else
                Debug.LogError("CardsStackPresenter: view is null");
            
            UpdateCounterText();
        }

        public void Push(CardItem card)
        {
            if (card == null)
            {
                Debug.LogError("Attempted to push a null CardItem.");
                return;
            }

            if (view != null)
            {
                view.Push(card.gameObject, stack.Count);
                card.transform.localPosition = GetNextCardPosition();
            }
            
            stack.Push(card);
            UpdateCounterText();
        }
        
        public Vector3 GetNextCardPosition(int offset = 1)
        {
            if (view == null)
            {
                Debug.LogError("Stack root is not set. Cannot determine next card position.");
                return Vector3.zero;
            }

            return view.GetNextCardPosition(stack.Count + offset);
        }

        public CardItem Pop()
        {
            if (stack.Count == 0)
            {
                Debug.LogWarning("Attempted to pop from an empty card stack.");
                return null;
            }

            var card = stack.Pop();
            
            card.transform.SetParent(null, worldPositionStays: true);
            UpdateCounterText();
            
            return card;
        }

        public void Clear()
        {
            while (stack.Count > 0)
                stack.Pop().transform.SetParent(null, worldPositionStays: true);
            
            UpdateCounterText();
        }

        private void UpdateCounterText()
        {
            if (view != null)
                view.UpdateCounterText(stack.Count);
        }
    }
}