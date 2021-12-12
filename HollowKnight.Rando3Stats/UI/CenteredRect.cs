using HollowKnight.Rando3Stats.Util;
using UnityEngine;
using UnityEngine.UI;

namespace HollowKnight.Rando3Stats.UI
{
    public class CenteredRect : ArrangableElement
    {
        private readonly GameObject imgObj;

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

            imgObj.transform.SetParent(canvas.transform, false);
        }

        protected override Vector2 MeasureOverride()
        {
            return imgObj.GetComponent<RectTransform>().rect.size;
        }

        protected override void ArrangeOverride(Rect availableSpace)
        {
            RectTransform tx = imgObj.GetComponent<RectTransform>();

            // place the center of the transform at the center of the area
            (float cx, float cy) = availableSpace.center;
            Vector2 pos = GuiManager.MakeAnchorPosition(new Vector2(cx - DesiredSize.x / 2, cy - DesiredSize.y / 2), DesiredSize);
            tx.anchorMax = pos;
            tx.anchorMin = pos;
        }
    }
}
