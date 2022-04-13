using System.Collections.Generic;

namespace Hierarchy
{
    public static partial class HierarchyExtensions
    {
        public static IEnumerable<IHierarchyNode<TData>> GetAllSiblingNodes<TData>(this IHierarchyNode<TData> node)
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
        public static IEnumerable<TData?> GetAllSiblingNodesData<TData>(this IHierarchyNode<TData> node)
        {
            foreach (var sibling in node.GetAllSiblingNodes())
            {
                yield return sibling.Data;
            }
        }

        public static IEnumerable<IHierarchyNode<TData>> GetAllChildrenNodes<TData>(this IHierarchyNode<TData> node)
        {
            foreach (var child in node.Children)
            {
                yield return child;
                foreach (var grandChild in child.GetAllChildrenNodes())
                {
                    yield return grandChild;
                }
            }
        }
        public static IEnumerable<TData?> GetAllChildrenNodesData<TData>(this IHierarchyNode<TData> node)
        {
            foreach (var child in node.GetAllChildrenNodes())
            {
                yield return child.Data;
            }
        }

        public static IEnumerable<IHierarchyNode<TData>> GetAllParentNodes<TData>(this IHierarchyNode<TData> node)
        {
            if (node.Parent is null)
            {
                yield break;
            }

            yield return node.Parent;
            foreach (var grandParent in node.Parent.GetAllParentNodes())
            {
                yield return grandParent;
            }
        }
        public static IEnumerable<TData?> GetAllParentNodesData<TData>(this IHierarchyNode<TData> node)
        {
            foreach (var parent in node.GetAllParentNodes())
            {
                yield return parent.Data;
            }
        }

        public static IHierarchyNode<TData> GetRootNode<TData>(this IHierarchyNode<TData> node)
        {
            var parentNode = node.Parent;
            while (parentNode is not null)
            {
                node = parentNode;
                parentNode = node.Parent;
            }
            return node;
        }
        public static TData? GetRootNodeData<TData>(this IHierarchyNode<TData> node)
        {
            return node.GetRootNode().Data;
        }

        public static IEnumerable<IHierarchyNode<TData>> FromBranchToFlatNodeList<TData>(this IHierarchyNode<TData> node)
        {
            yield return node;
            foreach (var child in node.GetAllChildrenNodes())
            {
                yield return child;
            }
        }
        public static IEnumerable<TData?> FromBranchToFlatNodeDataList<TData>(this IHierarchyNode<TData> node)
        {
            foreach (var hierarchyNode in node.FromBranchToFlatNodeList())
            {
                yield return hierarchyNode.Data;
            }
        }

        public static IEnumerable<IHierarchyNode<TData>> GetLeafNodes<TData>(this IHierarchyNode<TData> node)
        {
            if (node.Children is null || node.Children.Count == 0)
            {
                yield return node;
                yield break;
            }
            foreach (var child in node.Children)
            {
                foreach (var leaf in child.GetLeafNodes())
                {
                    yield return leaf;
                }
            }
        }
        public static IEnumerable<TData?> GetLeafNodesData<TData>(this IHierarchyNode<TData> node)
        {
            foreach (var leafNode in node.GetLeafNodes())
            {
                yield return leafNode.Data;
            }
        }

        public static IEnumerable<IHierarchyNode<TData>> ToFlatNodeList<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes)
        {
            foreach (var node in hierarchyNodes)
            {
                yield return node;
                foreach (var childNode in node.GetAllChildrenNodes())
                {
                    yield return childNode;
                }
            }
        }
        public static IEnumerable<TData?> ToFlatDataList<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes)
        {
            foreach (var hierarchyNode in hierarchyNodes.ToFlatNodeList())
            {
                yield return hierarchyNode.Data;
            }
        }
    }
}
