using Modding;
using UnityEngine;
using UnityEngine.UI;

namespace HollowKnight.Rando3Stats.UI
{
    public class GuiManager
    {
        public static readonly Vector2 ReferenceSize = new(1920, 1080);

        private static GuiManager? _instance;
        public static GuiManager Instance
        {
            get => _instance ??= new GuiManager();
        }

        public Font TrajanNormal { get; private set; }
        public Font TrajanBold { get; private set; }

        private GuiManager()
        {
            TrajanBold = CanvasUtil.TrajanBold;
            TrajanNormal = CanvasUtil.TrajanNormal;
        }

        /// <summary>
        /// Creates a new canvas game object that will be destroyed on scene transition
        /// </summary>
        public GameObject GetCanvasForScene(string name = "Canvas")
        {
            GameObject rootCanvas = new(name);
            rootCanvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            CanvasScaler scale = rootCanvas.AddComponent<CanvasScaler>();
            scale.referenceResolution = ReferenceSize;
            scale.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            rootCanvas.AddComponent<GraphicRaycaster>();
            CanvasGroup group = rootCanvas.AddComponent<CanvasGroup>();
            group.interactable = true;
            group.blocksRaycasts = true;
            return rootCanvas;
        }

        /// <summary>
        /// Converts a position in the reference screen space (1920x1080) to a transform anchor position
        /// </summary>
        /// <param name="pos">The position in screen space</param>
        /// <param name="pixelSize">The size in screen space</param>
        /// <returns>The transformed position</returns>
        public static Vector2 MakeAnchorPosition(Vector2 pos, Vector2 pixelSize)
        {
            return new((pos.x + pixelSize.x / 2f) / ReferenceSize.x,
                (ReferenceSize.y - (pos.y + pixelSize.y / 2f)) / ReferenceSize.y);
        }
    }
}
