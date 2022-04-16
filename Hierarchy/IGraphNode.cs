using System.Collections.Generic;

namespace Hierarchy
{
    public interface IGraphNode<TData>
    {
        public IList<IGraphNode<TData>> Parents { get; set; }
        public IList<IGraphNode<TData>> Children { get; set; }
        public TData Data { get; set; }
    }
}
