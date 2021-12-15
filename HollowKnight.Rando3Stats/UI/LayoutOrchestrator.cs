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

        public void RegisterElement(ArrangableElement element)
        {
            if (!elements.Contains(element))
            {
                elements.Add(element);
            }
        }

        private void Update()
        {
            // only update one element at a time
            ArrangableElement? element = elements.Where(x => x.LogicalParent == null)
                .FirstOrDefault(x => !x.MeasureIsValid);
            if (element != null)
            {
                log.LogDebug($"Triggering update for {element.Name} of type {element.GetType().Name}");
                element.PositionAt(element.DesiredPosition);
            }
        }
    }
}
