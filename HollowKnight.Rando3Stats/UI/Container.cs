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
        public Container(IArrangable child) : base()
        {
            Children.Add(child);
        }

        public override Vector2 DoMeasure()
        {
            CachedDesiredSize = Children[0].DoMeasure();
            return CachedDesiredSize;
        }

        public override void DoArrange(Rect availableSpace)
        {
            Children[0].DoArrange(availableSpace);
        }
    }
}
