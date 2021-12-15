using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HollowKnight.Rando3Stats.UI
{
    public class ParentedElementList : IEnumerable<ArrangableElement>
    {
        private readonly List<ArrangableElement> logicalChildren = new();
        private readonly ArrangableElement logicalParent;

        public ParentedElementList(ArrangableElement logicalParent)
        {
            this.logicalParent = logicalParent;
        }

        public ArrangableElement this[int index] 
        {
            get => logicalChildren[index]; 
            set => logicalChildren[index] = value; 
        }

        public int Count => logicalChildren.Count;

        public bool IsReadOnly => false;

        public void Add(ArrangableElement item)
        {
            if (logicalParent.VisualParent != item.VisualParent)
            {
                throw new ArgumentException("The element must be drawn on the same visual parent as its logical parent", nameof(item));
            }
            item.LogicalParent = logicalParent;
            logicalChildren.Add(item);
            logicalParent.InvalidateMeasure();
        }

        public bool Contains(ArrangableElement item)
        {
            return logicalChildren.Contains(item);
        }

        public IEnumerator<ArrangableElement> GetEnumerator()
        {
            return logicalChildren.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(ArrangableElement item)
        {
            return logicalChildren.IndexOf(item);
        }

        public void Insert(int index, ArrangableElement item)
        {
            item.LogicalParent = logicalParent;
            logicalChildren.Insert(index, item);
            logicalParent.InvalidateMeasure();
        }
    }
}
