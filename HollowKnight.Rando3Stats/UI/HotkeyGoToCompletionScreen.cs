using HollowKnight.Rando3Stats.Util;
using UnityEngine;

namespace HollowKnight.Rando3Stats.UI
{
    internal class HotkeyGoToCompletionScreen : MonoBehaviour
    {
        private const string MODIFIER_CTRL = "ctrl";
        private const string MODIFIER_SHIFT = "shift";
        private bool GetModifier(string name)
        {
            return Input.GetKey("left " + name) || Input.GetKey("right " + name);
        }

        private void Update()
        {
            if (GameManager.instance.IsGamePaused() && GetModifier(MODIFIER_CTRL) && GetModifier(MODIFIER_SHIFT) && Input.GetKeyDown(KeyCode.C))
            {
                SkipToCompletionScreen.Start();
                Destroy(gameObject);
            }
        }
    }
}
