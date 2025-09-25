using UnityEngine;

namespace Game.Initial.Controller
{
    public class GameStarter: MonoBehaviour
    {
        [Range(30, 120)]
        public int TargetFps = 60;
        
        public void Awake()
        {
            Application.targetFrameRate = TargetFps;
        }
    }
}