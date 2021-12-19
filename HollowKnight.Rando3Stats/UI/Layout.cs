using UnityEngine;

namespace HollowKnight.Rando3Stats.UI
{
    /// <summary>
    /// An abstract layout for arranging several child elements.
    /// </summary>
    public abstract class Layout : ArrangableElement
    {
        public ParentedElementList Children { get; }

        public Layout(GameObject visualParent, string name) : base(visualParent, name)
        {
            Children = new ParentedElementList(this);
        }
    }
}
