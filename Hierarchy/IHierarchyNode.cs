using System.Collections.Generic;

namespace Hierarchy
{
    public interface IHierarchyNode<TData>
    {
        public IHierarchyNode<TData>? Parent { get; set; }
        public IList<IHierarchyNode<TData>> Children { get; set; }
        public TData Data { get; set; }
    }
}
