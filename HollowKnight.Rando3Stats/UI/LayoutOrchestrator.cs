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
        private readonly Dictionary<string, List<ArrangableElement>> elementLookup = new();

        public int measureBatch = 2;
        public int arrangeBatch = 5;

        public void RegisterElement(ArrangableElement element)
        {
            if (!elements.Contains(element))
            {
                elements.Add(element);
                if (!elementLookup.ContainsKey(element.Name))
                {
                    elementLookup[element.Name] = new List<ArrangableElement>();
                }
                elementLookup[element.Name].Add(element);
            }
        }

        public ArrangableElement? Find(string name)
        {
            if (elementLookup.ContainsKey(name))
            {
                return elementLookup[name].First();
            } 
            else
            {
                return null;
            }
        }

        public IEnumerable<ArrangableElement> FindAll(string name)
        {
            if (elementLookup.ContainsKey(name))
            {
                return elementLookup[name].AsReadOnly();
            }
            else 
            {
                return Enumerable.Empty<ArrangableElement>();
            }
        }

        public T? Find<T>(string name) where T : ArrangableElement
        {
            if (elementLookup.ContainsKey(name))
            {
                return elementLookup[name].OfType<T>().FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<T> FindAll<T>(string name) where T : ArrangableElement
        {
            if (elementLookup.ContainsKey(name))
            {
                return elementLookup[name].OfType<T>();
            }
            else
            {
                return Enumerable.Empty<T>();
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
