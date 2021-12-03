using UnityEngine;

namespace HollowKnight.Rando3Stats.Util
{
    public static class UnityExtensions
    {
        public static void Deconstruct(this Vector2 vec, out float x, out float y)
        {
            x = vec.x;
            y = vec.y;
        }
    }
}
