using UnityEngine;
using UnityEngine.UI;

namespace HollowKnight.Rando3Stats.UI
{
    public class CenteredText : IArrangable
    {
        private readonly GameObject textObj;
        private readonly string textStr;

        public Vector2 CachedDesiredSize { get; private set; }

        public CenteredText(GameObject canvas, string text, Font font, int fontSize, string name = "Text")
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

            CanvasGroup grp = textObj.AddComponent<CanvasGroup>();
            grp.interactable = false;
            grp.blocksRaycasts = false;

            Text textComponent = textObj.AddComponent<Text>();
            textComponent.font = font;
            textComponent.text = text;
            textComponent.fontSize = fontSize;
            textComponent.alignment = TextAnchor.UpperLeft;

            textObj.transform.SetParent(canvas.transform, false);
            // learning: no need for crazy state management (DontDestroyOnLoad) -- just create the text when you load into the scene and let
            // it destroy itself on the outbound transistion
        }

        public Vector2 DoMeasure()
        {
            Text textComponent = textObj.GetComponent<Text>();
            TextGenerator textGen = new();
            TextGenerationSettings settings = textComponent.GetGenerationSettings(textComponent.rectTransform.rect.size);
            float width = textGen.GetPreferredWidth(textStr, settings);
            float height = textGen.GetPreferredHeight(textStr, settings);

            CachedDesiredSize = new Vector2(width, height);
            return CachedDesiredSize;
        }

        public void DoArrange(Rect availableSpace)
        {
            RectTransform tx = textObj.GetComponent<RectTransform>();

            // place the center of the text transform at the center of the area
            (float cx, float cy) = availableSpace.center;
            Vector2 pos = GuiManager.MakeAnchorPosition(new Vector2(cx - CachedDesiredSize.x / 2, cy - CachedDesiredSize.y / 2),
                GuiManager.ReferenceSize);
            tx.anchorMax = pos;
            tx.anchorMin = pos;
        }
    }
}
