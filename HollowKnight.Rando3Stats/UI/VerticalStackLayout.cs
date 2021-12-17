using HollowKnight.Rando3Stats.Util;
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

        public VerticalStackLayout(GameObject canvas, float spacing, 
            HorizontalAlignment horizontalAlign = HorizontalAlignment.Left,
            VerticalAlignment verticalAlign = VerticalAlignment.Top,
            string name = nameof(VerticalStackLayout)) : base(canvas, name)
        {
            this.spacing = spacing;
            HorizontalAlignment = horizontalAlign;
            VerticalAlignment = verticalAlign;
        }

        protected override Vector2 MeasureOverride()
        {
            if (Children.Count == 0) return Vector2.zero;
            float width = 0;
            float height = (Children.Count - 1) * spacing;
            foreach (ArrangableElement child in Children)
            {
                (float childWidth, float childHeight) = child.DoMeasure();
                height += childHeight;
                if (childWidth > width)
                {
                    width = childWidth;
                }
            }
            return new Vector2(width, height);
        }

        protected override void ArrangeOverride(Rect availableSpace)
        {
            Vector2 topLeft = GetAlignedTopLeftCorner(availableSpace);

            (float left, float top) = topLeft;
            foreach (ArrangableElement child in Children)
            {
                float childHeight = child.DesiredSize.y;
                child.DoArrange(new Rect(left, top, DesiredSize.x, childHeight));
                top += childHeight + spacing;
            }
        }
    }
}
