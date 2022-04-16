using System;
using System.Collections.Generic;

namespace Hierarchy
{
    public static class TraversalExtensions
    {
        public static IEnumerable<TData> BreadthFirstSearch<TData>(this TData node, Func<TData, IEnumerable<TData>> neighbors)
        {
            var queue = new Queue<TData>();
            var visited = new HashSet<TData>();
            queue.Enqueue(node);

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                if (visited.Contains(currentNode))
                {
                    continue;
                }
                visited.Add(currentNode);

                yield return currentNode;
                foreach (var neighbor in neighbors(currentNode))
                {
                    queue.Enqueue(neighbor);
                }
            }
        }
        public static IEnumerable<TData> DepthFirstSearch<TData>(this TData node, Func<TData, IEnumerable<TData>> neighbors)
        {
            var queue = new Stack<TData>();
            var visited = new HashSet<TData>();
            queue.Push(node);

            while (queue.Count > 0)
            {
                var currentNode = queue.Pop();
                if (visited.Contains(currentNode))
                {
                    continue;
                }
                visited.Add(currentNode);

                yield return currentNode;
                foreach (var neighbor in neighbors(currentNode))
                {
                    queue.Push(neighbor);
                }
            }
        }

        public static IEnumerable<IGraphNode<TData>> BreadthSearch<TData>(this IGraphNode<TData> node)
        {
            return node.BreadthFirstSearch(n => n.NeighborNodes());
        }

        public static IEnumerable<IGraphNode<TData>> DepthSearch<TData>(this IGraphNode<TData> node)
        {
            return node.DepthFirstSearch(n => n.NeighborNodes());
        }

        public static IEnumerable<IGraphNode<TData>> Search<TData>(this IGraphNode<TData> node, TraversalType traversalType = TraversalType.BreadthFirst)
        {
            switch (traversalType)
            {
                case TraversalType.BreadthFirst:
                    return node.BreadthSearch();
                case TraversalType.DepthFirst:
                    return node.DepthSearch();
                default:
                    break;
            }
            return null;
        }        

        public static IEnumerable<IHierarchyNode<TData>> BreadthSearch<TData>(this IHierarchyNode<TData> node)
        {
            return node.BreadthFirstSearch(n => n.Children);
        }

        public static IEnumerable<IHierarchyNode<TData>> DepthSearch<TData>(this IHierarchyNode<TData> node)
        {
            return node.DepthFirstSearch(n => n.Children);
        }

        public static IEnumerable<IHierarchyNode<TData>> Search<TData>(this IHierarchyNode<TData> node, TraversalType traversalType = TraversalType.BreadthFirst)
        {
            switch (traversalType)
            {
                case TraversalType.BreadthFirst:
                    return node.BreadthSearch();
                case TraversalType.DepthFirst:
                    return node.DepthSearch();
                default:
                    break;
            }
            return null;
        }
    }
}
