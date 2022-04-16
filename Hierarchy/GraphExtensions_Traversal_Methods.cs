using System;
using System.Collections.Generic;
using System.Linq;

namespace Hierarchy
{
    public static partial class GraphExtensions
    {
        public static IEnumerable<IGraphNode<TData>> NeighborNodes<TData>(this IGraphNode<TData> node)
        {
            return node.Children.Concat(node.Parents);
        }

        public static bool IsLinked<TData>(this IGraphNode<TData> node, Func<TData, bool> comparer, TraversalType traversalType = TraversalType.BreadthFirst)
        {
            foreach (var currentNode in node.Search(traversalType))
            {
                if (comparer(currentNode.Data))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsLinked<TData>(this IGraphNode<TData> node, IGraphNode<TData> searchNode)
        {
            return node.IsLinked(n => n.Equals(searchNode.Data));
        }

        public static IEnumerable<TData> ToFlatDataList<TData>(this IGraphNode<TData> graphNode, TraversalType traversalType = TraversalType.BreadthFirst)
        {
            foreach (var node in graphNode.Search(traversalType))
            {
                yield return node.Data;
            }
        }

        public static IEnumerable<TData> ToFlatDataList<TData>(this IEnumerable<IGraphNode<TData>> sourceNodes, TraversalType traversalType = TraversalType.BreadthFirst)
        {
            foreach (var branchNode in sourceNodes)
            {
                foreach (var nodeData in branchNode.ToFlatDataList())
                {
                    yield return nodeData;
                }
            }
        }
    }
}
