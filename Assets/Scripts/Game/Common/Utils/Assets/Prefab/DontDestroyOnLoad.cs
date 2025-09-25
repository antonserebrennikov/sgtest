using UnityEngine;

namespace Game.Common.Utils.Assets.Prefab
{
    public class DontDestroyOnLoad: MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}