using HollowKnight.Rando3Stats.Util;
using System;
using UnityEngine;

namespace HollowKnight.Rando3Stats.UI
{
    /// <summary>
    /// A dynamic uniform grid. Each child will be placed in a sub-panel according to the size of the largest child
    /// and the specified spacings, with a dynamic number of rows based on the specified maximum number of columns 
    /// and number of children. If the last row is not full, it will be aligned appropriately within the grid based
    /// on the specified alignment.
    /// </summary>
    public class DynamicGridLayout : Layout
    {
        private readonly float horizontalSpacing;
        private readonly float verticalSpacing;
        private readonly int maxColumns;

        public DynamicGridLayout(GameObject canvas,
            float horizontalSpacing, float verticalSpacing, int maxColumns, 
            HorizontalAlignment horizontalAlign = HorizontalAlignment.Left,
            VerticalAlignment verticalAlign = VerticalAlignment.Top,
            string name = nameof(DynamicGridLayout)) : base(canvas, name)
        {
            if (maxColumns < 1)
            {
                throw new ArgumentException("Need at least 1 column", nameof(maxColumns));
            }
            this.horizontalSpacing = horizontalSpacing;
            this.verticalSpacing = verticalSpacing;
            this.maxColumns = maxColumns;
            HorizontalAlignment = horizontalAlign;
            VerticalAlignment = verticalAlign;
        }

        protected override Vector2 MeasureOverride()
        {
            if (Children.Count == 0) return Vector2.zero;

            float panelWidth = 0;
            float panelHeight = 0;
            foreach (ArrangableElement child in Children)
            {
                (float childWidth, float childHeight) = child.DoMeasure();
                if (childWidth > panelWidth)
                {
                    panelWidth = childWidth;
                }
                if (childHeight > panelHeight)
                {
                    panelHeight = childHeight;
                }
            }
            int numRows = (Children.Count - 1) / maxColumns + 1;
            int numCols = Children.Count >= maxColumns ? maxColumns : Children.Count;

            return new Vector2(numCols * panelWidth + (numCols - 1) * horizontalSpacing,
                numRows * panelHeight + (numRows - 1) * verticalSpacing);
        }

        protected override void ArrangeOverride(Rect availableSpace)
        {
            int numRows = (Children.Count - 1) / maxColumns + 1;
            int numCols = Children.Count >= maxColumns ? maxColumns : Children.Count;

            float panelWidth = (DesiredSize.x - (numCols - 1) * horizontalSpacing) / numCols;
            float panelHeight = (DesiredSize.y - (numRows - 1) * verticalSpacing) / numRows;

            (_, float top) = GetAlignedTopLeftCorner(availableSpace);

            for (int row = 0; row < numRows; row++)
            {
                float startY = row * (panelHeight + verticalSpacing) + top;
                int childrenAvailable = Children.Count - row * numCols;
                int childrenThisRow = childrenAvailable >= maxColumns ? maxColumns : childrenAvailable;
                float widthOfRow = childrenThisRow * panelWidth + (childrenThisRow - 1) * horizontalSpacing;
                for (int col = 0; col < childrenThisRow; col++)
                {
                    // we can't just use the default aligned left side as the last row may be smaller than the others.
                    float startX = col * (panelWidth + horizontalSpacing) + HorizontalAlignment switch
                    {
                        HorizontalAlignment.Left => availableSpace.xMin,
                        HorizontalAlignment.Center => availableSpace.xMin + availableSpace.width / 2 - widthOfRow / 2,
                        HorizontalAlignment.Right => availableSpace.xMax - widthOfRow,
                        _ => throw new NotImplementedException("Can't handle the current horizontal alignment")
                    };
                    int childIndex = row * maxColumns + col;
                    Children[childIndex].DoArrange(new Rect(startX, startY, panelWidth, panelHeight));
                }
            }
        }
    }
}
