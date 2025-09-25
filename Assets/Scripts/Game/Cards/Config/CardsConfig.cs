using UnityEngine;

namespace Game.Cards.Config
{
    [CreateAssetMenu(fileName = "CardsConfig", menuName = "Game/Cards/Cards Config")]
    public class CardsConfig : ScriptableObject
    {
        [Header("Prefabs")][SerializeField]
        private string cardPrefabPath;
        
        [SerializeField]
        private string cardStackPrefabPath;

        [Header("Count")][Min(1)][SerializeField]
        private int cardsCount = 10;
        
        [SerializeField]
        private Vector2 cardsOffset = new Vector2(0.2f, 0f);

        [Header("Animation")][Min(0f)][SerializeField]
        private float animationTime = 1f;
        
        [SerializeField]
        private float cardPickTime = 1f;

        public string CardPrefabPath => cardPrefabPath;
        public string CardStackPrefabPath => cardStackPrefabPath;
        public int CardsCount => cardsCount;
        public Vector2 CardsOffset => cardsOffset;
        public float AnimationTime => animationTime;
        public float CardPickTime => cardPickTime;
    }
}