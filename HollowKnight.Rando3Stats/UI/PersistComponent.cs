using UnityEngine;

namespace HollowKnight.Rando3Stats.UI
{
    internal class PersistComponent : MonoBehaviour
    {
        internal void Destroy()
        {
            Destroy(gameObject);
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
