using UnityEngine;

namespace HollowKnight.Rando3Stats.UI
{
    /// <summary>
    /// An arrangable UI element
    /// </summary>
    public interface IArrangable
    {
        /// <summary>
        /// The cached desired size. By contract, this should be set in <see cref="DoMeasure"/>.
        /// </summary>
        public Vector2 CachedDesiredSize { get; }

        /// <summary>
        /// Calculates the desired size of the object and caches it in <see cref="CachedDesiredSize"/> for later reference in this UI build.
        /// </summary>
        public Vector2 DoMeasure();

        /// <summary>
        /// Positions the object within the allocated space.
        /// </summary>
        /// <param name="availableSpace">The space available for the element.</param>
        public void DoArrange(Rect availableSpace);
    }
}
