using HollowKnight.Rando3Stats.Util;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HollowKnight.Rando3Stats.UI
{
    public class CenteredText : ArrangableElement
    {
        private readonly GameObject textObj;
        private string textStr;

        public string Text
        {
            get => textStr;
            set
            {
                if (value != textStr)
                {
                    textStr = value;
                    textObj.GetComponent<Text>().text = value;
                    InvalidateMeasure();
                }
            }
        }

        public CenteredText(GameObject canvas, string text, Font font, int fontSize, string name = "Text") : base(canvas, name)
        {
            textStr = text;

            textObj = new GameObject(name);
            textObj.AddComponent<CanvasRenderer>();

            // note - it is (apparently) critically important to add the transform before the text.
            // Otherwise the text won't show (presumably because it's not transformed properly?)
            Vector2 pos = GuiManager.MakeAnchorPosition(new(0, 0), GuiManager.ReferenceSize);
            RectTransform tx = textObj.AddComponent<RectTransform>();
            tx.sizeDelta = GuiManager.ReferenceSize;
            tx.anchorMin = pos;
            tx.anchorMax = pos;

            Text textComponent = textObj.AddComponent<Text>();
            textComponent.font = font;
            textComponent.text = text;
            textComponent.fontSize = fontSize;
            textComponent.alignment = TextAnchor.UpperLeft;

            textObj.transform.SetParent(canvas.transform, false);
            // hide until the first arrange
            textObj.SetActive(false);
            // learning: no need for crazy state management (DontDestroyOnLoad) -- just create the text when you load into the scene and let
            // it destroy itself on the outbound transistion
        }

        protected override Vector2 MeasureOverride()
        {
            Text textComponent = textObj.GetComponent<Text>();
            TextGenerator textGen = new();
            TextGenerationSettings settings = textComponent.GetGenerationSettings(textComponent.rectTransform.rect.size);
            // by default, this will inherit the parent canvas's scale factor, which is set to scale with screen space.
            // however, since we're functioning in an unscaled coordinate system we should get the unscaled size to measure correctly.
            settings.scaleFactor = 1;
            float width = textGen.GetPreferredWidth(textStr, settings);
            float height = textGen.GetPreferredHeight(textStr, settings);

            return new Vector2(width, height);
        }

        protected override void ArrangeOverride(Rect availableSpace)
        {
            RectTransform tx = textObj.GetComponent<RectTransform>();

            // place the center of the text transform at the center of the area
            (float cx, float cy) = availableSpace.center;
            Vector2 pos = GuiManager.MakeAnchorPosition(new Vector2(cx - DesiredSize.x / 2, cy - DesiredSize.y / 2),
                GuiManager.ReferenceSize);
            tx.anchorMax = pos;
            tx.anchorMin = pos;

            textObj.SetActive(true);
        }
    }
}
