using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hierarchy
{
    public static partial class HierarchyExtensions
    {
        public static THierarchyNode AddChild<THierarchyNode, TData>(this THierarchyNode sourceNode, TData childData)
            where THierarchyNode : IHierarchyNode<TData>, new()
        {
            sourceNode.Children.Add(new THierarchyNode() { Data = childData, Parent = sourceNode });
            return sourceNode;
        }

        public static THierarchyNode AddChild<THierarchyNode, TData>(this THierarchyNode sourceNode, THierarchyNode childNode)
            where THierarchyNode : IHierarchyNode<TData>, new()
        {
            childNode.Parent = sourceNode;
            sourceNode.Children.Add(childNode);
            return sourceNode;
        }

        public static IEnumerable<IHierarchyNode<TData>> SiblingNodes<TData>(this IHierarchyNode<TData> sourceNode)
        {
            if (sourceNode.Parent is null)
            {
                yield break;
            }

            foreach (var child in sourceNode.Parent.Children)
            {
                if (child != sourceNode)
                {
                    yield return child;
                }
            }
        }
        public static IEnumerable<TData> SiblingNodesData<TData>(this IHierarchyNode<TData> sourceNode)
        {
            foreach (var sibling in sourceNode.SiblingNodes())
            {
                yield return sibling.Data;
            }
        }

        public static IEnumerable<IHierarchyNode<TData>> DescendantNodes<TData>(this IHierarchyNode<TData> sourceNode, TraversalType traversalType = TraversalType.BreadthFirst)
        {
            // Skip the first node because the search always returns to source node first
            foreach (var node in sourceNode.Search(traversalType).Skip(1))
            {
                yield return node;
            }
        }
        public static IEnumerable<TData> DescendantNodesData<TData>(this IHierarchyNode<TData> sourceNode, TraversalType traversalType = TraversalType.BreadthFirst)
        {
            foreach (var descendantNode in sourceNode.DescendantNodes(traversalType))
            {
                yield return descendantNode.Data;
            }
        }

        public static IEnumerable<IHierarchyNode<TData>> AncestorNodes<TData>(this IHierarchyNode<TData> sourceNode)
        {
            while (sourceNode.Parent != null)
            {
                yield return sourceNode.Parent;
                sourceNode = sourceNode.Parent;
            }
        }
        public static IEnumerable<TData> AncestorNodesData<TData>(this IHierarchyNode<TData> sourceNode)
        {
            foreach (var ancestorNode in sourceNode.AncestorNodes())
            {
                yield return ancestorNode.Data;
            }
        }

        public static IHierarchyNode<TData> RootNode<TData>(this IHierarchyNode<TData> sourceNode)
        {
            while (sourceNode.Parent != null)
            {
                sourceNode = sourceNode.Parent;
            }
            return sourceNode;
        }
        public static TData RootNodeData<TData>(this IHierarchyNode<TData> sourceNode)
        {
            return sourceNode.RootNode().Data;
        }

        public static IEnumerable<IHierarchyNode<TData>> ToFlatNodeList<TData>(this IHierarchyNode<TData> sourceNode, TraversalType traversalType = TraversalType.BreadthFirst)
        {
            foreach (var node in sourceNode.Search(traversalType))
            {
                yield return node;
            }
        }

        public static IEnumerable<TData> ToFlatNodeDataList<TData>(this IHierarchyNode<TData> sourceNode, TraversalType traversalType = TraversalType.BreadthFirst)
        {
            foreach (var node in sourceNode.ToFlatNodeList(traversalType))
            {
                yield return node.Data;
            }
        }

        public static IEnumerable<IHierarchyNode<TData>> LeafNodes<TData>(this IHierarchyNode<TData> sourceNode, TraversalType traversalType = TraversalType.BreadthFirst)
        {
            foreach (var node in sourceNode.Search(traversalType))
            {
                if (node.Children is null || node.Children.Count == 0)
                {
                    yield return node;
                }
            }
        }
        public static IEnumerable<TData> LeafNodesData<TData>(this IHierarchyNode<TData> sourceNode, TraversalType traversalType = TraversalType.BreadthFirst)
        {
            foreach (var node in sourceNode.LeafNodes(traversalType))
            {
                yield return node.Data;
            }
        }

        public static IEnumerable<IHierarchyNode<TData>> ToFlatNodeList<TData>(this IEnumerable<IHierarchyNode<TData>> sourceNodes, TraversalType traversalType = TraversalType.BreadthFirst)
        {
            foreach (var nodeBranch in sourceNodes)
            {
                foreach (var node in nodeBranch.Search(traversalType))
                {
                    yield return node;
                }
            }
        }
        public static IEnumerable<TData> ToFlatDataList<TData>(this IEnumerable<IHierarchyNode<TData>> sourceNodes, TraversalType traversalType = TraversalType.BreadthFirst)
        {
            foreach (var node in sourceNodes.ToFlatNodeList(traversalType))
            {
                yield return node.Data;
            }
        }

        public static IEnumerable<IHierarchyNode<TData>> AllNodes<TData>(this IEnumerable<IHierarchyNode<TData>> sourceNodes, TraversalType traversalType = TraversalType.BreadthFirst)
        {
            return sourceNodes.ToFlatNodeList(traversalType);
        }

        public static string PrintTree<TData>(this IEnumerable<IHierarchyNode<TData>> sourceNodes, int level = 0, string indenter = "│   ", int indentSize = 0, StringBuilder sb = null)
        {
            if (sb == null)
            {
                sb = new StringBuilder();
                indentSize = indenter.Length;
            }

            var isLastNode = false;
            var indenterBuffer = indenter;
            foreach (var node in sourceNodes)
            {
                if (level > 0)
                {
                    if (level > 1)
                    {
                        sb.Append(indenter);
                    }

                    if (node.Parent.Children.Last() == node)
                    {
                        sb.AppendLine($"└─ {node}");
                        isLastNode = true;
                    }
                    else
                    {
                        sb.AppendLine($"├─ {node}");
                    }
                    if (level > 1)
                    {
                        indenterBuffer = isLastNode ? $"{indenter}{new string(' ', indentSize)}" : $"{indenter}{indenter}";
                    }
                }
                else
                {
                    sb.AppendLine(node.ToString());
                }
                node.Children.PrintTree(level + 1, indenterBuffer, indentSize, sb);
            }

            return sb.ToString();
        }

        public static string PrintNodes<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes)
        {
            var sb = new StringBuilder();

            foreach (var node in hierarchyNodes)
            {
                sb.AppendLine(node.ToString());
            }

            return sb.ToString();
        }
    }
}
