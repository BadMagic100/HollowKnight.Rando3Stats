using Modding;
using System;
using UnityEngine;

namespace HollowKnight.Rando3Stats.UI
{
    /// <summary>
    /// An arrangable UI element
    /// </summary>
    public abstract class ArrangableElement
    {
        protected static SimpleLogger log = new("RandoStats:ArrangableElement");

        private bool neverMeasured = true;
        private bool neverArranged = true;

        internal Rect PrevPlacementRect { get; private set; }

        public bool MeasureIsValid { get; private set; } = false;

        public bool ArrangeIsValid { get; private set; } = false;

        public string Name { get; private set; }

        private HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left;
        public HorizontalAlignment HorizontalAlignment
        {
            get => horizontalAlignment;
            set
            {
                if (horizontalAlignment != value)
                {
                    horizontalAlignment = value;
                    InvalidateArrange();
                }
            }
        }

        private VerticalAlignment verticalAlignment = VerticalAlignment.Top;
        public VerticalAlignment VerticalAlignment
        {
            get => verticalAlignment;
            set
            {
                if (verticalAlignment != value)
                {
                    verticalAlignment = value;
                    InvalidateArrange();
                }
            }
        }

        /// <summary>
        /// The cached desired size. Set from the last result in <see cref="DoMeasure"/>.
        /// </summary>
        public Vector2 DesiredSize { get; private set; }

        /// <summary>
        /// The cached desired position. Set from <see cref="PositionAt(Vector2)"/>
        /// </summary>
        public Vector2 DesiredPosition { get; private set; }

        /// <summary>
        /// This element's parent, if any.
        /// </summary>
        public ArrangableElement? LogicalParent { get; internal set; } = null;

        public GameObject VisualParent { get; private set; }

        public ArrangableElement(GameObject visualParent, string name)
        {
            LayoutOrchestrator? orch = visualParent.GetComponent<LayoutOrchestrator>();
            if (orch == null)
            {
                throw new ArgumentException("Visual parent must have a LayoutOrchestrator component to perform layout", nameof(visualParent));
            }
            Name = name;
            VisualParent = visualParent;
            orch.RegisterElement(this);
        }

        /// <summary>
        /// Indicates the measure is no longer valid; will trigger a full re-render of the visual tree.
        /// </summary>
        public void InvalidateMeasure()
        {
            MeasureIsValid = false;
            LogicalParent?.InvalidateMeasure();
        }

        public void InvalidateArrange()
        {
            ArrangeIsValid = false;
        }

        /// <summary>
        /// Helper method to get the position of the top left corner during arrangement, given the component's vertical and horizontal alignments.
        /// </summary>
        protected Vector2 GetAlignedTopLeftCorner(Rect availableSpace)
        {
            float x = horizontalAlignment switch
            {
                HorizontalAlignment.Left => availableSpace.xMin,
                HorizontalAlignment.Center => availableSpace.xMin + availableSpace.width / 2 - DesiredSize.x / 2,
                HorizontalAlignment.Right => availableSpace.xMax - DesiredSize.x,
                _ => throw new NotImplementedException("Can't handle the current horizontal alignment"),
            };

            float y = verticalAlignment switch
            {
                VerticalAlignment.Top => availableSpace.yMin,
                VerticalAlignment.Center => availableSpace.yMin + availableSpace.height / 2 - DesiredSize.y / 2,
                VerticalAlignment.Bottom => availableSpace.yMax - DesiredSize.y,
                _ => throw new NotImplementedException("Can't handle the current horizontal alignment"),
            };

            return new Vector2(x, y);
        }

        /// <summary>
        /// Calculates the desired size of the object and caches it in <see cref="DesiredSize"/> for later reference in this UI build cycle.
        /// </summary>
        public Vector2 DoMeasure()
        {
            if (!MeasureIsValid)
            {
                if (!neverMeasured)
                {
                    log.LogDebug($"Re-measure triggered for {Name}");
                }
                DesiredSize = MeasureOverride();
                MeasureIsValid = true;
                neverMeasured = false;
                InvalidateArrange();
                LogicalParent?.InvalidateMeasure();
            }
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
            // only rearrange if we're either put into a new space or explicitly told to rearrange.
            if (!ArrangeIsValid || PrevPlacementRect != availableSpace)
            {
                if (!neverArranged)
                {
                    log.LogDebug($"Re-arrange triggered for {Name}");
                }
                ArrangeOverride(availableSpace);
                neverArranged = false;
                PrevPlacementRect = availableSpace;
                ArrangeIsValid = true;
            }
        }

        /// <summary>
        /// Internal implementation to position the object within the allocated space.
        /// </summary>
        /// <param name="availableSpace">The space available for the element.</param>
        protected abstract void ArrangeOverride(Rect availableSpace);

        /// <summary>
        /// Positions this element at the given anchor point. This will be respected as long as this element does not have a parent.
        /// </summary>
        /// <param name="anchor">The anchoring position for this element</param>
        public void PositionAt(Vector2 anchor)
        {
            DesiredPosition = anchor;
            DoMeasure();
            // Layouts can & will use more space than allowed; the size of the rect is only used for alignment purposes.
            // At the parent level, we can set it as zero.
            DoArrange(new Rect(anchor, Vector2.zero));
        }
    }
}
