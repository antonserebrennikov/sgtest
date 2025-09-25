using UnityEngine;

namespace Game.Common.Utils.Debug
{
    public class FpsCounter : MonoBehaviour
    {
        [SerializeField][Range(0.01f, 3f)]
        private float UpdateInterval = 1f;

        [SerializeField][Range(24, 64)]
        private int FontSize = 48;
        
        [SerializeField]
        private Vector2 Margin = new Vector2(10f, 10f);

        private int frameCount;
        private float timeAccumUnscaled;
        private float fps;

        private void Update()
        {
            frameCount++;
            timeAccumUnscaled += Time.unscaledDeltaTime;

            if (timeAccumUnscaled < UpdateInterval)
                return;
            
            fps = frameCount / timeAccumUnscaled;
            frameCount = 0;
            timeAccumUnscaled = 0f;
        }

        private void OnGUI()
        {
            var style = new GUIStyle(GUI.skin.label)
            {
                fontSize = FontSize,
                alignment = TextAnchor.UpperLeft,
                normal =
                {
                    textColor = Color.white
                }
            };

            var text = $"{fps:F1} FPS";
            var size = style.CalcSize(new GUIContent(text));
            var rect = CalculateRect(size);

            GUI.Label(rect, text, style);
        }

        private Rect CalculateRect(Vector2 size)
        {
            return new Rect(Margin.x, Margin.y, size.x, size.y);
        }
    }
}
