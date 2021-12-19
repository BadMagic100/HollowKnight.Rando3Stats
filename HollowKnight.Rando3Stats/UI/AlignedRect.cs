using HollowKnight.Rando3Stats.Util;
using UnityEngine;
using UnityEngine.UI;

namespace HollowKnight.Rando3Stats.UI
{
    public class AlignedRect : ArrangableElement
    {
        private readonly GameObject imgObj;
        private readonly RectTransform tx;
        private float width, height;

        public float Width
        {
            get => width;
            set
            {
                if (value != width)
                {
                    width = value;
                    tx.SetScaleX(value / tx.sizeDelta.x);
                    InvalidateMeasure();
                }
            }
        }

        public float Height
        {
            get => height;
            set
            {
                if (value != height)
                {
                    height = value;
                    tx.SetScaleY(value / tx.sizeDelta.y);
                    InvalidateMeasure();
                }
            }
        }

        public AlignedRect(GameObject canvas, Color color, float width, float height, string name = "Rect") : base(canvas, name)
        {
            this.width = width;
            this.height = height;

            imgObj = new GameObject(name);
            imgObj.AddComponent<CanvasRenderer>();

            Vector2 size = new(width, height);
            Vector2 pos = GuiManager.MakeAnchorPosition(new(0, 0), size);
            tx = imgObj.AddComponent<RectTransform>();
            tx.sizeDelta = size;
            tx.anchorMin = pos;
            tx.anchorMax = pos;

            imgObj.AddComponent<Image>().color = color;

            imgObj.transform.SetParent(canvas.transform, false);
            if (canvas.GetComponent<PersistComponent>() != null)
            {
                imgObj.AddComponent<PersistComponent>();
            }

            // hide until the first arrange cycle
            imgObj.SetActive(false);
        }

        protected override Vector2 MeasureOverride()
        {
            return imgObj.GetComponent<RectTransform>().rect.size;
        }

        protected override void ArrangeOverride(Rect availableSpace)
        {
            RectTransform tx = imgObj.GetComponent<RectTransform>();

            // place the center of the transform at the center of the area
            Vector2 pos = GuiManager.MakeAnchorPosition(GetAlignedTopLeftCorner(availableSpace), DesiredSize);
            tx.anchorMax = pos;
            tx.anchorMin = pos;

            imgObj.SetActive(true);
        }
    }
}
