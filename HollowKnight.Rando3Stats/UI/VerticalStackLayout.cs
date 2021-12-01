using System;
using UnityEngine;

namespace HollowKnight.Rando3Stats.UI
{
    /// <summary>
    /// A vertically oriented stack. Children are arranged in a stack with the specified spacing between them.
    /// </summary>
    public class VerticalStackLayout : Layout
    {
        private readonly float spacing;
        private readonly HorizontalAlignment horizontalAlign;

        public VerticalStackLayout(float spacing, 
            HorizontalAlignment horizontalAlign = HorizontalAlignment.Left) : base()
        {
            this.spacing = spacing;
            this.horizontalAlign = horizontalAlign;
        }

        public override Vector2 DoMeasure()
        {
            if (Children.Count == 0) return Vector2.zero;
            float width = 0;
            float height = (Children.Count - 1) * spacing;
            foreach (IArrangable child in Children)
            {
                (float childWidth, float childHeight) = child.DoMeasure();
                height += childHeight;
                if (childWidth > width)
                {
                    width = childWidth;
                }
            }
            CachedDesiredSize = new Vector2(width, height);
            return CachedDesiredSize;
        }

        public override void DoArrange(Rect availableSpace)
        {
            float startY = availableSpace.yMin;

            float xMin = horizontalAlign switch
            {
                HorizontalAlignment.Left => availableSpace.xMin,
                HorizontalAlignment.Center => availableSpace.xMin + availableSpace.width / 2 - CachedDesiredSize.x / 2,
                HorizontalAlignment.Right => availableSpace.xMax - CachedDesiredSize.x,
                _ => throw new NotImplementedException("Can't handle the current Horizontal alignment")
            };
            foreach (IArrangable child in Children)
            {
                float childHeight = child.CachedDesiredSize.y;
                child.DoArrange(new Rect(xMin, startY, CachedDesiredSize.x, childHeight));
                startY += childHeight + spacing;
            }
        }
    }
}
