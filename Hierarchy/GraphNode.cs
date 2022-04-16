using System.Collections.Generic;

namespace Hierarchy
{
    public class GraphNode<TData> : IGraphNode<TData>
    {  
        public IList<IGraphNode<TData>> Parents { get; set; } = new List<IGraphNode<TData>>();
        public IList<IGraphNode<TData>> Children { get; set; } = new List<IGraphNode<TData>>();
        public TData Data { get; set; } = default;
    

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}
