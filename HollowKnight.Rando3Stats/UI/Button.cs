using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UButton = UnityEngine.UI.Button;

namespace HollowKnight.Rando3Stats.UI
{
    public class Button : ArrangableElement
    {
        private readonly GameObject imgObj;
        private readonly GameObject? textObj;
        private readonly RectTransform tx;
        private readonly UButton btn;
        private float width, height;

        private void InvokeClick()
        {
            Click?.Invoke(this);
        }

        public event UnityAction<Button>? Click;

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

        public bool Enabled
        {
            get => btn.interactable;
            set => btn.interactable = value;
        }

        public Button(GameObject canvas, Color fill, float width, float height, string? text = null, Font? font = null, int fontSize = 0, string name = "Button") : base(canvas, name)
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

            imgObj.AddComponent<Image>().color = fill;
            btn = imgObj.AddComponent<UButton>();
            btn.onClick.AddListener(InvokeClick);

            imgObj.transform.SetParent(canvas.transform, false);

            if (font != null && text != null)
            {
                textObj = new GameObject();
                textObj.AddComponent<RectTransform>().sizeDelta = new Vector2(width, height);
                Text t = textObj.AddComponent<Text>();
                t.text = text;
                t.font = font;
                t.fontSize = fontSize;
                t.alignment = TextAnchor.MiddleCenter;
                textObj.transform.SetParent(imgObj.transform, false);
            }

            if (canvas.GetComponent<PersistComponent>() != null)
            {
                imgObj.AddComponent<PersistComponent>();
                textObj?.AddComponent<PersistComponent>();
            }

            // hide until the first arrange cycle
            imgObj.SetActive(false);
        }

        public Button(GameObject canvas, Texture2D background, string? text = null, Font? font = null, int fontSize = 0, string name = "Button") : base(canvas, name)
        {
            width = background.width;
            height = background.height;

            imgObj = new GameObject(name);
            imgObj.AddComponent<CanvasRenderer>();

            Vector2 size = new(width, height);
            Vector2 pos = GuiManager.MakeAnchorPosition(new(0, 0), size);
            tx = imgObj.AddComponent<RectTransform>();
            tx.sizeDelta = size;
            tx.anchorMin = pos;
            tx.anchorMax = pos;

            imgObj.AddComponent<Image>().sprite = Sprite.Create(
                background,
                new Rect(0, 0, width, height),
                Vector2.zero
            );
            btn = imgObj.AddComponent<UButton>();
            btn.onClick.AddListener(InvokeClick);

            imgObj.transform.SetParent(canvas.transform, false);

            if (font != null && text != null)
            {
                textObj = new GameObject();
                textObj.AddComponent<RectTransform>().sizeDelta = new Vector2(width, height);
                Text t = textObj.AddComponent<Text>();
                t.text = text;
                t.font = font;
                t.fontSize = fontSize;
                t.alignment = TextAnchor.MiddleCenter;
                textObj.transform.SetParent(imgObj.transform, false);
            }

            if (canvas.GetComponent<PersistComponent>() != null)
            {
                imgObj.AddComponent<PersistComponent>();
                textObj?.AddComponent<PersistComponent>();
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
