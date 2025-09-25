using System;
using System.Threading.Tasks;
using DG.Tweening;
using Game.Cards.Config;
using Game.Cards.Presenter;
using Game.Common.Utils.Assets.Prefab;
using Jnk.TinyContainer;
using UnityEngine;

namespace Game.Cards.Controller
{
    public class CardsController: MonoBehaviour
    {
        [SerializeField]
        private Transform placeholder1;
        
        [SerializeField]
        private Transform placeholder2;
        
        [SerializeField]
        private CardsConfig cardsConfig;
        
        private IPrefabLoader prefabLoader;
        private CardsStackPresenter stack1;
        private CardsStackPresenter stack2;
        private bool isInialized;
        private float elapsedTime;
        private CardsShuffleCompletedPresenter shuffleCompletedPresenter;
        private int animationsInProgress;

        private void Start()
        {
            if (cardsConfig == null)
                Debug.LogError($"{nameof(cardsConfig)} cannot be null");

            if (placeholder1 == null)
                Debug.LogError($"{nameof(placeholder1)} cannot be null");

            if (placeholder2 == null)
                Debug.LogError($"{nameof(placeholder2)} cannot be null");
            
            TinyContainer.For(this).Get(out prefabLoader);
        }
        
        public async Task InitAsync()
        {
            try
            {
                await InitCardStacksAsync();
                await InitCardsAsync();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            
            TinyContainer.For(this).Get(out shuffleCompletedPresenter);
            isInialized = true;
        }
        
        public async Task DeinitAsync()
        {
            stack1.Clear();
            stack2.Clear();
            shuffleCompletedPresenter?.Hide();
            //TODO: add unloading assets from twen animations
        }

        private async Task InitCardStacksAsync()
        {
            //TODO: add objects pooling
            var prefab1 = await prefabLoader.LoadAsync(cardsConfig.CardStackPrefabPath);
            
            stack1 = prefab1.GetComponent<CardsStackPresenter>();
            prefab1.transform.SetParent(placeholder1);
            prefab1.transform.localPosition = Vector3.zero;
            stack1.Init(cardsConfig.CardsOffset);
            
            var prefab2 = await prefabLoader.LoadAsync(cardsConfig.CardStackPrefabPath);
            
            stack2 = prefab2.GetComponent<CardsStackPresenter>();
            prefab2.transform.SetParent(placeholder2);
            prefab2.transform.localPosition = Vector3.zero;
            stack2.Init(cardsConfig.CardsOffset);
        }
        
        private async Task InitCardsAsync()
        {
            //TODO: add objects pooling
            for (int i = 0; i < cardsConfig.CardsCount; i++)
            {
                var prefab = await prefabLoader.LoadAsync(cardsConfig.CardPrefabPath);
                var view = prefab.GetComponent<CardItem>();
            
                stack1.Push(view);
            }
        }

        private void Update()
        {
            elapsedTime += Time.deltaTime;
            
            if (!isInialized)
                return;

            if (stack1.Count <= 0)
            {
                if (animationsInProgress <= 0)
                {
                    shuffleCompletedPresenter?.Show();
                    isInialized = false;
                }
                
                return;
            }

            if (elapsedTime < cardsConfig.CardPickTime)
                return;
            
            animationsInProgress++;
            elapsedTime = 0;
            
            //TODO: handle errors / nulls when scene was unloaded during async operations
            var card = stack1.Pop();
            var localTarget = stack2.GetNextCardPosition(animationsInProgress);
            var target = stack2.transform.TransformPoint(localTarget);
                
            card.transform.position = new Vector3(card.transform.position.x, card.transform.position.y, -10);
            card.transform.DOMove(target, cardsConfig.AnimationTime)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    stack2?.Push(card);
                    animationsInProgress--;
                });
        }
    }
}