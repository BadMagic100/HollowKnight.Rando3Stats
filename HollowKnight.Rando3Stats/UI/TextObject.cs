using UnityEngine;
using UnityEngine.UI;

namespace HollowKnight.Rando3Stats.UI
{
    public class TextObject : ArrangableElement
    {
        private readonly GameObject textObj;
        private readonly Text textComponent;
        private readonly RectTransform tx;

        public string Text
        {
            get => textComponent.text;
            set
            {
                if (value != textComponent.text)
                {
                    textComponent.text = value;
                    tx.sizeDelta = MeasureText();
                    InvalidateMeasure();
                }
            }
        }

        public TextAnchor TextAlignment
        {
            get => textComponent.alignment;
            set
            {
                if (value != textComponent.alignment)
                {
                    textComponent.alignment = value;
                    tx.sizeDelta = MeasureText();
                    InvalidateMeasure();
                }
            }
        }

        public TextObject(GameObject canvas, string text, Font font, int fontSize, string name = "Text") : base(canvas, name)
        {
            textObj = new GameObject(name);
            textObj.AddComponent<CanvasRenderer>();

            // note - it is (apparently) critically important to add the transform before the text.
            // Otherwise the text won't show (presumably because it's not transformed properly?)
            Vector2 pos = GuiManager.MakeAnchorPosition(new(0, 0), GuiManager.ReferenceSize);
            tx = textObj.AddComponent<RectTransform>();
            tx.anchorMin = pos;
            tx.anchorMax = pos;

            textComponent = textObj.AddComponent<Text>();
            textComponent.font = font;
            textComponent.text = text;
            textComponent.fontSize = fontSize;
            textComponent.horizontalOverflow = HorizontalWrapMode.Overflow;
            textComponent.verticalOverflow = VerticalWrapMode.Overflow;
            textComponent.alignment = TextAnchor.UpperLeft;
            tx.sizeDelta = MeasureText();

            textObj.transform.SetParent(canvas.transform, false);
            if (canvas.GetComponent<PersistComponent>() != null)
            {
                textObj.AddComponent<PersistComponent>();
            }

            // hide until the first arrange
            textObj.SetActive(false);
        }

        private Vector2 MeasureText()
        {
            TextGenerator textGen = new();
            // have as much space as the screen for the text; otherwise we risk unwanted clipping
            TextGenerationSettings settings = textComponent.GetGenerationSettings(GuiManager.ReferenceSize);
            // by default, this will inherit the parent canvas's scale factor, which is set to scale with screen space.
            // however, since we're functioning in an unscaled coordinate system we should get the unscaled size to measure correctly.
            settings.scaleFactor = 1;
            float width = textGen.GetPreferredWidth(textComponent.text, settings);
            float height = textGen.GetPreferredHeight(textComponent.text, settings);
            return new Vector2(width, height);
        }

        protected override Vector2 MeasureOverride()
        {
            return MeasureText();
        }

        protected override void ArrangeOverride(Rect availableSpace)
        {
            RectTransform tx = textObj.GetComponent<RectTransform>();

            // place the center of the text transform at the center of the area
            Vector2 pos = GuiManager.MakeAnchorPosition(GetAlignedTopLeftCorner(availableSpace), DesiredSize);
            tx.anchorMax = pos;
            tx.anchorMin = pos;

            textObj.SetActive(true);
        }
    }
}
