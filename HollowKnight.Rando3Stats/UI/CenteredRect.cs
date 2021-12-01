using UnityEngine;
using UnityEngine.UI;

namespace HollowKnight.Rando3Stats.UI
{
    public class CenteredRect : IArrangable
    {
        private readonly GameObject imgObj;

        public Vector2 CachedDesiredSize { get; private set; }

        public CenteredRect(GameObject canvas, Color color, Vector2 size, string name = "Rect")
        {
            imgObj = new GameObject(name);
            imgObj.AddComponent<CanvasRenderer>();

            Vector2 pos = GuiManager.MakeAnchorPosition(new(0, 0), size);
            RectTransform tx = imgObj.AddComponent<RectTransform>();
            tx.sizeDelta = size;
            tx.anchorMin = pos;
            tx.anchorMax = pos;

            imgObj.AddComponent<Image>().color = color;

            CanvasGroup group = imgObj.AddComponent<CanvasGroup>();
            group.interactable = false;
            group.blocksRaycasts = false;

            imgObj.transform.SetParent(canvas.transform, false);
        }

        public Vector2 DoMeasure()
        {
            CachedDesiredSize = imgObj.GetComponent<RectTransform>().rect.size;
            return CachedDesiredSize;
        }

        public void DoArrange(Rect availableSpace)
        {
            RectTransform tx = imgObj.GetComponent<RectTransform>();

            // place the center of the transform at the center of the area
            (float cx, float cy) = availableSpace.center;
            Vector2 pos = GuiManager.MakeAnchorPosition(new Vector2(cx - CachedDesiredSize.x / 2, cy - CachedDesiredSize.y / 2), CachedDesiredSize);
            tx.anchorMax = pos;
            tx.anchorMin = pos;
        }
    }
}
