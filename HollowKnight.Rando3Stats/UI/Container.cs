using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace HollowKnight.Rando3Stats.UI
{
    /// <summary>
    /// A simple layout containing a single child.
    /// </summary>
    public class Container : Layout
    {
        public Container(ArrangableElement child) : base()
        {
            Children.Add(child);
        }

        protected override Vector2 MeasureOverride()
        {
            return Children[0].DoMeasure();
        }

        protected override void ArrangeOverride(Rect availableSpace)
        {
            Children[0].DoArrange(availableSpace);
        }
    }
}
