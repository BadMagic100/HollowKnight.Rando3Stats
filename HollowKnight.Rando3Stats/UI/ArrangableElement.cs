using UnityEngine;

namespace HollowKnight.Rando3Stats.UI
{
    /// <summary>
    /// An arrangable UI element
    /// </summary>
    public abstract class ArrangableElement
    {
        /// <summary>
        /// The cached desired size. Set from the last result in <see cref="DoMeasure"/>.
        /// </summary>
        public Vector2 DesiredSize { get; private set; }

        /// <summary>
        /// Calculates the desired size of the object and caches it in <see cref="DesiredSize"/> for later reference in this UI build cycle.
        /// </summary>
        public Vector2 DoMeasure()
        {
            DesiredSize = MeasureOverride();
            return DesiredSize;
        }

        /// <summary>
        /// Internal implementation to calculate desired size.
        /// </summary>
        protected abstract Vector2 MeasureOverride();

        /// <summary>
        /// Positions the object within the allocated space.
        /// </summary>
        /// <param name="availableSpace">The space available for the element.</param>
        public void DoArrange(Rect availableSpace)
        {
            // mirroring WPF-style structure; account for the fact we might need to do other stuff in the future here.
            ArrangeOverride(availableSpace);
        }

        /// <summary>
        /// Internal implementation to position the object within the allocated space.
        /// </summary>
        /// <param name="availableSpace">The space available for the element.</param>
        protected abstract void ArrangeOverride(Rect availableSpace);
    }
}
