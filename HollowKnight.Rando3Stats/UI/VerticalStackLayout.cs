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
        private readonly HorizontalAlignment horizontalAlign;
        private readonly VerticalAlignment verticalAlign;

        public VerticalStackLayout(GameObject canvas, float spacing, 
            HorizontalAlignment horizontalAlign = HorizontalAlignment.Left,
            VerticalAlignment verticalAlign = VerticalAlignment.Top,
            string name = nameof(VerticalStackLayout)) : base(canvas, name)
        {
            this.spacing = spacing;
            this.horizontalAlign = horizontalAlign;
            this.verticalAlign = verticalAlign;
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
            float startY = verticalAlign switch
            {
                VerticalAlignment.Top => availableSpace.yMin,
                VerticalAlignment.Center => availableSpace.yMin + availableSpace.height / 2 - DesiredSize.y / 2,
                VerticalAlignment.Bottom => availableSpace.yMax - DesiredSize.y,
                _ => throw new NotImplementedException("Can't handle the current vertical alignment")
            };

            float xMin = horizontalAlign switch
            {
                HorizontalAlignment.Left => availableSpace.xMin,
                HorizontalAlignment.Center => availableSpace.xMin + availableSpace.width / 2 - DesiredSize.x / 2,
                HorizontalAlignment.Right => availableSpace.xMax - DesiredSize.x,
                _ => throw new NotImplementedException("Can't handle the current horizontal alignment")
            };
            foreach (ArrangableElement child in Children)
            {
                float childHeight = child.DesiredSize.y;
                child.DoArrange(new Rect(xMin, startY, DesiredSize.x, childHeight));
                startY += childHeight + spacing;
            }
        }
    }
}
