using System.Collections.Generic;

namespace Hierarchy
{
    public class HierarchyNode<TData> : IHierarchyNode<TData>
    {
        public IHierarchyNode<TData>? Parent { get; set; }
        public IList<IHierarchyNode<TData>> Children { get; set; } = new List<IHierarchyNode<TData>>();
        public TData? Data { get; set; }
    }
}
