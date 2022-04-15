using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hierarchy
{
    public static partial class HierarchyExtensions
    {
        public static THierarchyNode AddChild<THierarchyNode, TData>(this THierarchyNode node, TData childData)
            where THierarchyNode : IHierarchyNode<TData>, new()
        {
            node.Children.Add(new THierarchyNode() { Data = childData, Parent = node });
            return node;
        }

        public static THierarchyNode AddChild<THierarchyNode, TData>(this THierarchyNode node, THierarchyNode childNode)
            where THierarchyNode : IHierarchyNode<TData>, new()
        {
            childNode.Parent = node;
            node.Children.Add(childNode);
            return node;
        }

        public static IEnumerable<IHierarchyNode<TData>> SiblingNodes<TData>(this IHierarchyNode<TData> node)
        {
            if (node.Parent is null)
            {
                yield break;
            }

            foreach (var child in node.Parent.Children)
            {
                if (child != node)
                {
                    yield return child;
                }
            }
        }
        public static IEnumerable<TData> SiblingNodesData<TData>(this IHierarchyNode<TData> node)
        {
            foreach (var sibling in node.SiblingNodes())
            {
                yield return sibling.Data;
            }
        }

        public static IEnumerable<IHierarchyNode<TData>> DescendantNodes<TData>(this IHierarchyNode<TData> node)
        {
            foreach (var child in node.Children)
            {
                yield return child;
                foreach (var grandChild in child.DescendantNodes())
                {
                    yield return grandChild;
                }
            }
        }
        public static IEnumerable<TData> DescendantNodesData<TData>(this IHierarchyNode<TData> node)
        {
            foreach (var child in node.DescendantNodes())
            {
                yield return child.Data;
            }
        }

        public static IEnumerable<IHierarchyNode<TData>> AncestorNodes<TData>(this IHierarchyNode<TData> node)
        {
            if (node.Parent is null)
            {
                yield break;
            }

            yield return node.Parent;
            foreach (var grandParent in node.Parent.AncestorNodes())
            {
                yield return grandParent;
            }
        }
        public static IEnumerable<TData> AncestorNodesData<TData>(this IHierarchyNode<TData> node)
        {
            foreach (var parent in node.AncestorNodes())
            {
                yield return parent.Data;
            }
        }

        public static IHierarchyNode<TData> RootNode<TData>(this IHierarchyNode<TData> node)
        {
            var parentNode = node.Parent;
            while (parentNode is not null)
            {
                node = parentNode;
                parentNode = node.Parent;
            }
            return node;
        }
        public static TData RootNodeData<TData>(this IHierarchyNode<TData> node)
        {
            return node.RootNode().Data;
        }

        public static IEnumerable<IHierarchyNode<TData>> FromNodeToFlatNodeList<TData>(this IHierarchyNode<TData> node)
        {
            yield return node;
            foreach (var child in node.DescendantNodes())
            {
                yield return child;
            }
        }
        public static IEnumerable<TData> FromNodeToFlatNodeDataList<TData>(this IHierarchyNode<TData> node)
        {
            foreach (var hierarchyNode in node.FromNodeToFlatNodeList())
            {
                yield return hierarchyNode.Data;
            }
        }

        public static IEnumerable<IHierarchyNode<TData>> LeafNodes<TData>(this IHierarchyNode<TData> node)
        {
            if (node.Children is null || node.Children.Count == 0)
            {
                yield return node;
                yield break;
            }
            foreach (var child in node.Children)
            {
                foreach (var leaf in child.LeafNodes())
                {
                    yield return leaf;
                }
            }
        }
        public static IEnumerable<TData> LeafNodesData<TData>(this IHierarchyNode<TData> node)
        {
            foreach (var leafNode in node.LeafNodes())
            {
                yield return leafNode.Data;
            }
        }

        public static IEnumerable<IHierarchyNode<TData>> ToFlatNodeList<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes)
        {
            foreach (var node in hierarchyNodes)
            {
                yield return node;
                foreach (var childNode in node.DescendantNodes())
                {
                    yield return childNode;
                }
            }
        }
        public static IEnumerable<TData> ToFlatDataList<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes)
        {
            foreach (var hierarchyNode in hierarchyNodes.ToFlatNodeList())
            {
                yield return hierarchyNode.Data;
            }
        }

        public static IEnumerable<IHierarchyNode<TData>> AllNodes<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes)
        {
            return hierarchyNodes.ToFlatNodeList();
        }

        public static string PrintTree<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes, int level = 0, string indenter = "│   ", int indentSize = 0, StringBuilder sb = null)
        {
            if (sb == null)
            {
                sb = new StringBuilder();
                indentSize = indenter.Length;
            }

            var isLastNode = false;
            var indenterBuffer = indenter;
            foreach (var node in hierarchyNodes)
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
