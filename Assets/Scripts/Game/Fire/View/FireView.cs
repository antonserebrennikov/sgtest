using MVP.View;
using UnityEngine;

namespace Game.Fire.View
{
    public class FireView: MonoBehaviour, IView
    {
        [SerializeField]
        private Animator animator;

        private void Awake()
        {
            if (animator == null)
                Debug.LogError("Animator component is missing on FireView");
        }
        
        public void OnNextButtonClick()
        {
            animator?.SetTrigger("Next");
        }
    }
}