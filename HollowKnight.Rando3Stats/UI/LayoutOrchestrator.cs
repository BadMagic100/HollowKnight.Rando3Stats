using Modding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace HollowKnight.Rando3Stats.UI
{
    public class LayoutOrchestrator : MonoBehaviour
    {
        private static SimpleLogger log = new("RandoStats:LayoutOrchestrator");

        private readonly List<ArrangableElement> elements = new();

        public int measureBatch = 2;
        public int arrangeBatch = 5;

        public void RegisterElement(ArrangableElement element)
        {
            if (!elements.Contains(element))
            {
                elements.Add(element);
            }
        }

        private void Update()
        {
            // remeasure the specified number of elements. since measure invalidation propagates up the visual tree,
            // we can take only elements that have no parents (i.e. are roots of trees).
            IEnumerable<ArrangableElement> elementsToRemeasure = elements
                .Where(x => x.LogicalParent == null && !x.MeasureIsValid)
                .Take(measureBatch);
            foreach (ArrangableElement element in elementsToRemeasure)
            {
                log.LogDebug($"Triggering remeasure/arrange for {element.Name} of type {element.GetType().Name}");
                element.PositionAt(element.DesiredPosition);
            }

            // rearrange the specified number of elements. arrange invalidation does not propagate up the tree, so we can generally
            // process larger batches.
            IEnumerable<ArrangableElement> elementsToRearrange = elements
                .Where(x => !x.ArrangeIsValid)
                .Take(arrangeBatch);
            foreach (ArrangableElement element in elementsToRearrange)
            {
                log.LogDebug($"Triggering rearrange for {element.Name} of type {element.GetType().Name}");
                // an invalidated arrange indicates the element wants to place itself in a different location within the same available space.
                element.DoArrange(element.PrevPlacementRect);
            }
        }
    }
}
