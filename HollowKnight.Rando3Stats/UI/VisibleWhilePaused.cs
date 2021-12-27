using UnityEngine;

namespace HollowKnight.Rando3Stats.UI
{
    internal class VisibleWhilePaused : MonoBehaviour
    {
        private void Update()
        {
            if (GameManager.instance.IsGamePaused())
            {
                gameObject.SetActiveChildren(true);
            }
            else
            {
                gameObject.SetActiveChildren(false);
            }
        }
    }
}
