using System.Collections.Generic;
using UnityEngine;

namespace HollowKnight.Rando3Stats.UI
{
    /// <summary>
    /// An abstract layout for arranging several child elements.
    /// </summary>
    public abstract class Layout : ArrangableElement
    {
        public List<ArrangableElement> Children { get; } = new List<ArrangableElement>();

        /// <summary>
        /// Starts measuring and arranging the layout tree with this as the parent.
        /// </summary>
        /// <param name="anchor">The anchoring position for this layout</param>
        public void DoLayout(Vector2 anchor)
        {
            DoMeasure();
            // Layouts can & will use more space than allowed; the size of the rect is only used for alignment purposes.
            // At the parent level, we can set it as zero.
            DoArrange(new Rect(anchor, Vector2.zero));
        }
    }
}
